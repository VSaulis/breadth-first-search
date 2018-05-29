using System;
using System.Collections.Generic;

namespace BreadthFirstSearch {
    public class BreadthFirstSearch {
        private readonly Maze _maze;
        private readonly Maze _resultMaze;
        private int _stepsCount;
        private readonly List<Directions> _pathRules;
        private readonly List<Tuple<int, int>> _pathPeaks;

        public BreadthFirstSearch(string filename) 
        {
            _pathRules = new List<Directions>();
            _pathPeaks = new List<Tuple<int, int>>();
            _resultMaze = new Maze(filename);
            _maze = new Maze(filename);
        }

        public bool Start(int i, int j) {
            if (!_maze.IsPositionValid(i, j)) {
                return false;
            }
            _maze.SetStatus(2, i, j);
            _resultMaze.SetStatus(2, i, j);
            List<Tuple<int, int>> peaks = new List<Tuple<int, int>>(){ new Tuple<int, int>(i, j) };
            return Wave(peaks, 3);
        }

        private bool Wave(List<Tuple<int, int>> peaks, int step) {

            Log.AddToLog("New wave with step : "+step);
            Log.AddToLog("-----------------------------------------");

            List<Tuple<int, int>> newPeaks = new List<Tuple<int, int>>();

            foreach (var peak in peaks) {

                Log.AddToLog("New peak : [" + peak.Item1 + ", " + peak.Item2 + "]");

                foreach (Directions direction in Enum.GetValues(typeof(Directions))) {

                    Tuple<int, int> newPosition = Move(direction, peak.Item1, peak.Item2);

                    if (_maze.IsPositionValid(newPosition.Item1, newPosition.Item2)) {

                        if (_maze.IsPositionAvailable(newPosition.Item1, newPosition.Item2)) {

                            _maze.SetStatus(step, newPosition.Item1, newPosition.Item2);
                            _stepsCount++;
                            newPeaks.Add(newPosition);

                            if (_maze.IsMazeEdge(newPosition.Item1, newPosition.Item2)) {
                                PrintAnswer(step, newPosition.Item1, newPosition.Item2);
                                return true;
                            }

                            Log.AddToLog(_stepsCount + ". Step : " + step + ". New peak found : ["+newPosition.Item1 + ", " + newPosition.Item2 + "]");
                        }
                        else {
                            _stepsCount++;
                            Log.AddToLog(_stepsCount + ". Step : " + step + ". Try to move to unavailable position.");
                        }
                    }
                    else {
                        _stepsCount++;
                        Log.AddToLog(_stepsCount + ". Step : " + step + ". Try to move out of maze.");
                    }
                }

                Log.AddToLog("");
            }

            Log.AddToLog("-----------------------------------------");
            Log.AddToLog("");
            Log.AddToLog("");

            if (newPeaks.Count == 0) {
                if (step < 10) {
                    _resultMaze.PrintMaze();
                }
                else {
                    _resultMaze.PrintMazeBig();
                }
                return false;
            }

            return Wave(newPeaks, step+1);
        }

        private void PrintAnswer(int step, int i, int j) {

            Log.AddToLog(_stepsCount + ". " + "Step : " + step + ". Maze edge reached.");
            _resultMaze.SetStatus(step, i, j);

            CollectPath(step, i, j);

            if (step < 10) {
                _maze.PrintMaze();
                _resultMaze.PrintMaze();
            }
            else {
                _maze.PrintMazeBig();
                _resultMaze.PrintMazeBig();
            }

            string pathInRules = "Path in rules : ";
            for (int k = _pathRules.Count - 1; k >= 0 ; k--) {
                pathInRules = pathInRules + (InverseDirection(_pathRules[k]));
                if (k != 0) {
                    pathInRules = pathInRules + " -> ";
                }
            }
            Log.AddToLog(pathInRules);

            string pathInPeaks = "Path in peaks : ";
            for (int k = _pathPeaks.Count - 1; k >= 0; k--) {
                pathInPeaks = pathInPeaks + "[" + _pathPeaks[k].Item1 + ", " + _pathPeaks[k].Item2 + "] ";
            }
            Log.AddToLog(pathInPeaks);
        }

        private void CollectPath(int step, int i, int j) {

            if (step == 2) {
                _pathPeaks.Add(new Tuple<int, int>(i, j));
                return;
            }

            foreach (Directions direction in Enum.GetValues(typeof(Directions))) {

                Tuple<int, int> newPosition = Move(direction, i, j);

                if (_maze.IsPositionValid(newPosition.Item1, newPosition.Item2)) {

                    if (_maze.GetMazePosition(newPosition.Item1, newPosition.Item2) == (step - 1)) {
                        step--;
                        _pathRules.Add(direction);
                        _pathPeaks.Add(new Tuple<int, int>(i, j));
                        _resultMaze.SetStatus(step, newPosition.Item1, newPosition.Item2);
                        CollectPath(step, newPosition.Item1, newPosition.Item2);
                        return;
                    }
                }
            }
        }

        private Tuple<int, int> Move(Directions direction, int i, int j) {
            switch (direction) {
                case Directions.Up:
                    i -= 1;
                    break;
                case Directions.Right:
                    j += 1;
                    break;
                case Directions.Bottom:
                    i += 1;
                    break;
                case Directions.Left:
                    j -= 1;
                    break;

            }
            return new Tuple<int, int>(i, j);
        }

        private Directions InverseDirection(Directions direction) {
            switch (direction) {
                case Directions.Up:
                    direction = Directions.Bottom;
                    break;
                case Directions.Right:
                    direction = Directions.Left;
                    break;
                case Directions.Bottom:
                    direction = Directions.Up;
                    break;
                case Directions.Left:
                    direction = Directions.Right;
                    break;

            }
            return direction;
        }
    }
}
