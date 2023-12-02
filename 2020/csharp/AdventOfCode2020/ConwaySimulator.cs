using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public interface IConwayCell
    {
        IEnumerable<IConwayCell> Neighbours { get; }
    }
    
    public class ConwaySimulator
    {
        public delegate bool NextCellState(bool currentState, int activeNeighbourCells);
        
        private readonly int _numberOfIterations;
        private readonly NextCellState _nextCellState;

        public ConwaySimulator(int numberOfIterations, NextCellState nextCellState)
        {
            _numberOfIterations = numberOfIterations;
            _nextCellState = nextCellState;
        }

        public IDictionary<IConwayCell, bool> Simulate(IDictionary<IConwayCell, bool> initialState)
        {
            var state = initialState;

            for (var i = 0; i < _numberOfIterations; i++)
            {
                foreach (var neighbour in state.Keys.SelectMany(x => x.Neighbours).Distinct().ToList())
                {
                    if (!state.ContainsKey(neighbour))
                        state[neighbour] = false;
                }

                var newState = new Dictionary<IConwayCell, bool>();
                foreach (var (point, isActive) in state)
                {
                    var activeNeighbours = point.Neighbours.Where(state.ContainsKey).Count(x => state[x]);
                    newState[point] = _nextCellState.Invoke(isActive, activeNeighbours);
                }

                state = newState;
            }

            return state;
        }
    }
}