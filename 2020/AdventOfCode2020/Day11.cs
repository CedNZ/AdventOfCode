using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day11 : IDay<Seat>
    {
        public List<Seat> SetupInputs(string[] inputs)
        {
            int seatRows = inputs.Length;
            int seatColumns = inputs.FirstOrDefault().Length;

            int count = 0;
            List<Seat> seats = new List<Seat>();

            Func<int, int, IEnumerable<(int Row, int Column)>> GetNeighbourPositions = (row, column) =>
            {
                var possibleRows = new[] { row - 1, row, row + 1 }.Where(x => x >= 0 && x <= seatRows);
                var possibleColumns = new[] { column - 1, column, column + 1 }.Where(x => x >= 0 && x <= seatColumns);

                return from r in possibleRows
                       from c in possibleColumns
                       select (r, c);
            };

            for (int row = 0; row < inputs.Length; row++)
            {
                string line = inputs[row];
                for (int column = 0; column < line.Length; column++)
                {

                    Seat newSeat = new Seat(line[column], row, column, count);
                    count++;
                    

                    //if(newSeat.IsFloor)
                    //{
                    //    continue;
                    //}

                    var neighbourPositions = GetNeighbourPositions(row, column);

                    foreach(var seat in seats.Where(s => neighbourPositions.Contains(s.Position)))
                    {
                        seat.Neighbours.Add(newSeat);
                        newSeat.Neighbours.Add(seat);
                    }
                    seats.Add(newSeat);
                }
            }

            return seats;
        }

        public long A(List<Seat> inputs)
        {
            bool stable = true;
            do
            {
                stable = true;
                foreach(var input in inputs)
                {
                    input.SetNextState(ref stable);
                }
                foreach(var input in inputs)
                {
                    input.SwitchState();
                }
            } while(!stable);

            return inputs.Count(x => x.Occupied);
        }

        public long B(List<Seat> inputs)
        {
            return -1;

            //var lastSeat = inputs.Last();
            //int maxX = lastSeat.Position.Column;
            //int maxY = lastSeat.Position.Row;

            //bool stable = true;
            //do
            //{
            //    stable = true;
            //    foreach(var input in inputs.Where(x => !x.IsFloor))
            //    {
            //        input.UpdateNeighbours(inputs, maxX, maxY);
            //    }
            //    foreach(var input in inputs.Where(x => !x.IsFloor))
            //    {
            //        input.SetNextState(ref stable);
            //    }
            //    foreach(var input in inputs.Where(x => !x.IsFloor))
            //    {
            //        input.SwitchState();
            //    }
            //    Debug.WriteLine($"Occupied {inputs.Count(x => x.Occupied)}");
            //} while(!stable);

            //return inputs.Count(x => x.Occupied);
        }
    }

    public class Seat
    {
        public SeatEnum SeatState;
        public SeatEnum NextState;

        public bool Occupied => SeatState == SeatEnum.Occupied;
        public bool Empty => SeatState == SeatEnum.Empty;
        public bool IsFloor => SeatState == SeatEnum.Floor;

        public List<Seat> Neighbours;

        public (int Row, int Column) Position;

        public int Id;

        public Seat(char input, int row, int column, int id)
        {
            SeatState = input switch
            {
                'L' => SeatEnum.Empty,
                '#' => SeatEnum.Occupied,
                '.' => SeatEnum.Floor,
                _ => throw new ArgumentException($"Invalid input of {input}")
            };
            NextState = SeatState;
            Neighbours = new List<Seat>();
            Position = (row, column);
            Id = id;
        }

        public void SetNextState(ref bool stabilised)
        {
            if (SeatState == SeatEnum.Floor)
            {
                return;
            }

            var emptyNeighbours = Neighbours.Count(x => x.Empty);
            var occupiedNeighbours = Neighbours.Count(x => x.Occupied);

            if (SeatState == SeatEnum.Empty)
            {
                if(occupiedNeighbours == 0)
                {
                    NextState = SeatEnum.Occupied;
                    stabilised = false;
                }
            }
            else if (SeatState == SeatEnum.Occupied)
            {
                if (occupiedNeighbours >= 4)
                {
                    NextState = SeatEnum.Empty;
                    stabilised = false;
                }
            }
        }

        public void SwitchState()
        {
            if(SeatState == SeatEnum.Floor)
            {
                return;
            }

            SeatState = NextState;
        }

        private List<(int, int)> neighbourOffset = new List<(int, int)>
        {
            (-1, -1), (-1, 0), (-1, +1),
            ( 0, -1),          ( 0, +1),
            (+1, -1), (+1, 0), (+1, +1),
        };

        public void UpdateNeighbours(List<Seat> allSeats, int maxX, int maxY)
        {
            Neighbours.Clear();

            foreach(var offset in neighbourOffset)
            {
                while(true)
                {
                    var position = NextPosition(Position, offset);
                    if(position.X > maxX || position.X < 0 || position.Y > maxY || position.Y < 0)
                    {
                        break;
                    }
                    var neighbour = allSeats.Where(s => s.Occupied).FirstOrDefault(s => s.Position == position && s.Occupied);
                    if (neighbour != null)
                    {
                        Neighbours.Add(neighbour);
                        break;
                    }
                }
            }
        }

        public (int X, int Y) NextPosition((int x, int y) position, (int x, int y) offset)
        {
            return (position.x + offset.x, position.y + offset.y);
        }

        public override string ToString()
        {
            return $"{Id}:{Position}, {SeatState}";
        }
    }

    public enum SeatEnum
    {
        Empty,
        Occupied,
        Floor
    }
}
