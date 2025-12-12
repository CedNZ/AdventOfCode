using AdventOfCodeCore;

namespace AoC_2025
{
    public class Day11 : IDay<Device>
    {
        public long A(List<Device> inputs)
        {
            Queue<Device> deviceQueue = [];
            deviceQueue.Enqueue(inputs.FirstOrDefault(x => x.Name == "you")!);

            var paths = 0;

            while (deviceQueue.TryDequeue(out var device))
            {
                if (device.IsOut)
                {
                    paths++;
                    continue;
                }

                foreach (var child in device.Children)
                {
                    deviceQueue.Enqueue(child);
                }
            }

            return paths;
        }

        int Paths(Device source, Device destination)
        {
            Queue<Device> deviceQueue = [];
            deviceQueue.Enqueue(source);

            var paths = 0;

            while (deviceQueue.TryDequeue(out var device))
            {
                if (device == destination)
                {
                    paths++;
                    continue;
                }

                foreach (var child in device.Children)
                {
                    deviceQueue.Enqueue(child);
                }
            }

            return paths;
        }

        public long B(List<Device> inputs)
        {
            var server = inputs.First(x => x.Name == "svr");
            var fft = inputs.First(x => x.Name == "fft");
            var dac = inputs.First(x => x.Name == "dac");
            var dest = inputs.Single(x => x.IsOut);

            var server_fftPaths = Paths(server, fft);
            var fft_dacPaths = Paths(fft, dac);
            var dac_outPaths = Paths(dac, dest);

            return server_fftPaths * fft_dacPaths * dac_outPaths;
        }

        public List<Device> SetupInputs(string[] inputs)
        {
            HashSet<Device> devices = [];
            foreach (var l in inputs)
            {
                var parts = l.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim(':')).ToList();
                var device = devices.FirstOrDefault(x => x.Name == parts[0], new Device(parts[0]));
                devices.Add(device);
                var names = parts.Skip(1).ToList();
                foreach (var name in names)
                {
                    var child = devices.FirstOrDefault(x => x.Name == name, new Device(name));
                    devices.Add(child);
                    device.Children.Add(child);
                }
            }

            return devices.ToList();
        }
    }

    public record Device
    {
        public string Name { get; }
        public bool IsOut { get; }
        public HashSet<Device> Children { get; } = [];

        public Device(string name)
        {
            Name = name;
            IsOut = name == "out";
        }
    }
}
