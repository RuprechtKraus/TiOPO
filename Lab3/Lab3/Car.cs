namespace Lab3
{
    public enum Directions
    {
        Forward,
        Backwards,
        None
    }

    public class Car
    {
        private static Dictionary<int, (int, int)> _gearSpeedRanges = new Dictionary<int, (int, int)>()
        {
            {-1, (-20, 0) },
            {0, (-20, 150) },
            {1, (0, 30) },
            {2, (20, 50) },
            {3, (30, 60) },
            {4, (40, 90) },
            {5, (50, 150) },
        };

        public bool IsTurnedOn { get; private set; }
        public int Speed { get; private set; }
        public int Gear { get; private set; }
        public Directions Direction { get; private set; }

        public Car()
        {
            IsTurnedOn = false;
            Speed = 0;
            Gear = 0;
            Direction = Directions.None;
        }

        public bool TurnOnEngine()
        {
            IsTurnedOn = true;
            return true;
        }

        public bool TurnOffEngine()
        {
            if (Gear == 0 && Speed == 0)
            {
                IsTurnedOn = false;
            }

            return !IsTurnedOn;
        }

        public bool SetGear(int gear)
        {
            if (!IsTurnedOn)
            {
                return false;
            }

            if (gear < -1 || gear > 5)
            {
                return false;
            }

            switch (gear)
            {
                case -1:
                    if (Speed != 0)
                    {
                        if (Gear != gear)
                        {
                            return false;
                        }
                    }
                    break;
                case 0:
                    break;
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    if (Speed < _gearSpeedRanges[gear].Item1 ||
                        Speed > _gearSpeedRanges[gear].Item2)
                    {
                        return false;
                    }
                    break;
            }

            Gear = gear;
            return true;
        }

        public bool SetSpeed(int speed)
        {
            if (!IsTurnedOn)
            {
                return false;
            }

            if (speed < 0 || speed > 150)
            {
                return false;
            }

            switch (Gear)
            {
                case -1:
                    if (-speed < _gearSpeedRanges[Gear].Item1)
                    {
                        return false;
                    }
                    Speed = -speed;
                    UpdateDirection();
                    return true;
                case 0:
                    if (speed > Math.Abs(Speed))
                    {
                        return false;
                    }
                    Speed = (speed > 0) ? speed : -speed;
                    UpdateDirection();
                    return true;
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    if (speed < _gearSpeedRanges[Gear].Item1 ||
                        speed > _gearSpeedRanges[Gear].Item2)
                    {
                        return false;
                    }
                    Speed = speed;
                    UpdateDirection();
                    return true;
                default:
                    return false;
            }
        }

        private void UpdateDirection()
        {
            if (Speed > 0)
            {
                Direction = Directions.Forward;
            }
            else if (Speed < 0)
            {
                Direction = Directions.Backwards;
            }
            else
            {
                Direction = Directions.None;
            }
        }
    }
}
