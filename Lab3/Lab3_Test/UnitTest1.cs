using Lab3;

namespace Lab3_Test
{
    public class Tests
    {
        private Car _car;

        [SetUp]
        public void Setup()
        {
            _car = new Car();
        }

        [Test]
        public void TurnOnEngine_WithEngineOff_TurnsOnEngine()
        {
            bool wasTurnedOn = _car.TurnOnEngine();

            Assert.That( wasTurnedOn );
            Assert.That( _car.IsTurnedOn );
        }

        [Test]
        public void TurnOffEngine_WithEngineOn_TurnsOffEngine()
        {
            _car.TurnOnEngine();
            bool wasTunedOff = _car.TurnOffEngine();

            Assert.That( wasTunedOff );
            Assert.That( !_car.IsTurnedOn );
        }

        [Test]
        public void TurnOffEngine_WhileMoving_CannotTurnOffEngine()
        {
            _car.TurnOnEngine();
            _car.SetGear( 1 );
            _car.SetSpeed( 10 );
            bool wasTurnedOff = _car.TurnOffEngine();

            Assert.That( !wasTurnedOff );
            Assert.That( _car.IsTurnedOn );
        }

        [Test]
        public void SetGear_ZeroGear_CarIsAtFirstGearAndZeroSpeedAndNoDirection()
        {
            _car.TurnOnEngine();
            _car.SetGear( 0 );

            Assert.Multiple( () =>
            {
                Assert.That( _car.Gear, Is.EqualTo( 0 ) );
                Assert.That( _car.Speed, Is.EqualTo( 0 ) );
                Assert.That( _car.Direction, Is.EqualTo( Directions.None ) );
            } );
        }

        [Test]
        public void SetGear_ReverseGearMovingForward_CannotSetGear()
        {
            _car.TurnOnEngine();
            _car.SetGear( 1 );
            _car.SetSpeed( 10 );

            bool wasGearSet = _car.SetGear( -1 );

            Assert.Multiple( () =>
            {
                Assert.That( wasGearSet, Is.False );
                Assert.That( _car.Gear, Is.EqualTo( 1 ) );
                Assert.That( _car.Direction, Is.EqualTo( Directions.Forward ) );
            } );
        }

        [Test]
        public void SetGear_FirstGearMovingBackwards_CannotSetGear()
        {
            _car.TurnOnEngine();
            _car.SetGear( -1 );
            _car.SetSpeed( 10 );

            bool wasGearSet = _car.SetGear( 1 );

            Assert.Multiple( () =>
            {
                Assert.That( wasGearSet, Is.False );
                Assert.That( _car.Gear, Is.EqualTo( -1 ) );
                Assert.That( _car.Direction, Is.EqualTo( Directions.Backwards ) );
            } );
        }

        [Test]
        public void SetGear_NegativeTwoGear_CannotSetGear()
        {
            _car.TurnOnEngine();

            bool wasGearSet = _car.SetGear( -2 );

            Assert.That( !wasGearSet );
            Assert.That( _car.Gear, Is.EqualTo( 0 ) );
        }

        [Test]
        public void SetGear_ExceedingMaxGear_CannotSetGear()
        {
            _car.TurnOnEngine();

            bool wasGearSet = _car.SetGear( 6 );

            Assert.That( !wasGearSet );
            Assert.That( _car.Gear, Is.EqualTo( 0 ) );
        }

        [Test]
        public void SetGear_WithEngineOff_CannotSetGear()
        {
            bool wasGearSet = _car.SetGear( 1 );

            Assert.That( !wasGearSet );
            Assert.That( _car.Gear, Is.EqualTo( 0 ) );
        }

        [Test]
        public void SetGear_WithNotEnoughSpeed_CannotSetGear()
        {
            _car.TurnOnEngine();
            _car.SetGear( 1 );
            _car.SetSpeed( 10 );
            bool wasGearSet = _car.SetGear( 2 );

            Assert.That( !wasGearSet );
            Assert.That( _car.Gear, Is.EqualTo( 1 ) );
        }

        [Test]
        public void SetGear_ZeroGearWhileMovingForwards_StillMovingForwards()
        {
            _car.TurnOnEngine();
            _car.SetGear( 1 );
            _car.SetSpeed( 10 );
            _car.SetGear( 0 );

            Assert.That( _car.Gear, Is.EqualTo( 0 ) );
            Assert.That( _car.Direction, Is.EqualTo( Directions.Forward ) );
        }

        [Test]
        public void SetGear_ZeroGearWhileMovingBackwards_StillMovingBackwards()
        {
            _car.TurnOnEngine();
            _car.SetGear( -1 );
            _car.SetSpeed( 10 );
            _car.SetGear( 0 );

            Assert.That( _car.Gear, Is.EqualTo( 0 ) );
            Assert.That( _car.Direction, Is.EqualTo( Directions.Backwards ) );
        }

        [Test]
        public void SetGear_ReverseGearWhileMovingBackwardsAtZeroGear_CannotSetGear()
        {
            _car.TurnOnEngine();
            _car.SetGear( -1 );
            _car.SetSpeed( 10 );
            _car.SetGear( 0 );

            bool wasGearSet = _car.SetGear( -1 );

            Assert.That( !wasGearSet );
            Assert.That( _car.Gear, Is.EqualTo( 0 ) );
        }

        [Test]
        public void SetSpeed_AtZeroGear_CannotSetSpeed()
        {
            _car.TurnOnEngine();

            bool wasSpeedSet = _car.SetGear( 0 );

            Assert.That( wasSpeedSet );
        }

        [Test]
        public void SetSpeed_IncreaseSpeedAtZeroGear_CannotSetSpeed()
        {
            _car.TurnOnEngine();

            bool wasSpeedSet = _car.SetSpeed( 10 );

            Assert.That( !wasSpeedSet );
            Assert.That( _car.Speed, Is.EqualTo( 0 ) );
        }

        [Test]
        public void SetSpeed_AtFirstGear_CarIsAtFirstGearAndForwardDirection()
        {
            _car.TurnOnEngine();
            _car.SetGear( 1 );
            _car.SetSpeed( 10 );

            Assert.Multiple( () =>
            {
                Assert.That( _car.Gear, Is.EqualTo( 1 ) );
                Assert.That( _car.Speed, Is.EqualTo( 10 ) );
                Assert.That( _car.Direction, Is.EqualTo( Directions.Forward ) );
            } );
        }

        [Test]
        public void SetSpeed_IncreaseSpeedWhileMoving_SpeedIncreased()
        {
            _car.TurnOnEngine();
            _car.SetGear( 1 );
            _car.SetSpeed( 10 );

            bool wasSpeedSet = _car.SetSpeed( 20 );

            Assert.That( wasSpeedSet );
            Assert.That( _car.Speed, Is.EqualTo( 20 ) );
        }

        [Test]
        public void SetSpeed_DecreaseSpeedWhileMoving_SpeedIncreaed()
        {
            _car.TurnOnEngine();
            _car.SetGear( 1 );
            _car.SetSpeed( 10 );

            bool wasSpeedSet = _car.SetSpeed( 5 );

            Assert.That( wasSpeedSet );
            Assert.That( _car.Speed, Is.EqualTo( 5 ) );
        }

        [Test]
        public void SetSpeed_AtZeroGear_CarIsAtReverseGearAndBackwardsDirection()
        {
            _car.TurnOnEngine();
            _car.SetGear( -1 );
            _car.SetSpeed( 10 );

            Assert.Multiple( () =>
            {
                Assert.That( _car.Gear, Is.EqualTo( -1 ) );
                Assert.That( _car.Speed, Is.EqualTo( -10 ) );
                Assert.That( _car.Direction, Is.EqualTo( Directions.Backwards ) );
            } );
        }

        [Test]
        public void SetSpeed_WithEngineOff_CannotSetSpeed()
        {
            bool wasSpeedSet = _car.SetSpeed( 10 );

            Assert.That( !wasSpeedSet );
            Assert.That( _car.Speed, Is.EqualTo( 0 ) );
        }

        [Test]
        public void SetSpeed_NegativeSpeed_CannotSetSpeed()
        {
            _car.TurnOnEngine();

            bool wasSpeedSet = _car.SetSpeed( -10 );

            Assert.That( !wasSpeedSet );
            Assert.That( _car.Speed, Is.EqualTo( 0 ) );
        }

        [Test]
        public void SetSpeed_NewSpeedExceedsGearUpperBound_CannotSetSpeed()
        {
            _car.TurnOnEngine();
            _car.SetGear( 1 );
            bool wasSpeedSet = _car.SetSpeed( 31 );

            Assert.That( !wasSpeedSet );
            Assert.That( _car.Speed, Is.EqualTo( 0 ) );
        }

        [Test]
        public void SetSpeed_NewSpeedLowerThanGearUpperBound_CannotSetSpeed()
        {
            _car.TurnOnEngine();
            _car.SetGear( 1 );
            _car.SetSpeed( 30 );
            _car.SetGear( 2 );
            bool wasSpeedSet = _car.SetSpeed( 10 );

            Assert.That( !wasSpeedSet );
            Assert.That( _car.Speed, Is.EqualTo( 30 ) );
        }
    }
}
