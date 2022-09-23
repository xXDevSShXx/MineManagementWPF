using DotSpatial.Positioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineManagementWPF
{
    public class GPS
    {
        public Position Position { get; set; }
        public Speed Speed { get; set; }
        public Azimuth Direction { get; set; }
        public DateTime DateTime { get; set; }

        private NmeaInterpreter interpreter;

        public void Start(Device device)
        {
            interpreter.Start(device);
        }

        public void Stop()
        {
            interpreter.Stop();
        }

        public GPS()
        {
            interpreter = new NmeaInterpreter();
            interpreter.PositionReceived += Interpreter_PositionReceived;
            interpreter.SpeedReceived += Interpreter_SpeedReceived;
            interpreter.HeadingReceived += Interpreter_HeadingReceived;
            interpreter.DateTimeChanged += Interpreter_DateTimeChanged;
        }

        private void Interpreter_DateTimeChanged(object sender, DateTimeEventArgs e)
        {
            DateTime = e.DateTime;
        }

        private void Interpreter_HeadingReceived(object sender, AzimuthEventArgs e)
        {
            Direction = e.Azimuth;
        }

        private void Interpreter_SpeedReceived(object sender, SpeedEventArgs e)
        {
            Speed = e.Speed;
        }

        private void Interpreter_PositionReceived(object sender, PositionEventArgs e)
        {
            Position = e.Position;
        }
    }
}
