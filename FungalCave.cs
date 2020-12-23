using System;

namespace XRL.World.ZoneBuilders
{
    public class helado_Mycopei_FungalCave
    {
        public const string OUTER_BLUEPRINT = "Mudroot";
        public const string INNER_BLUEPRINT = "helado_Mycopei_Fungal Wall";
        public const double OUTER_ELLIPSE_CENTER_X = 40.0;
        public const double OUTER_ELLIPSE_CENTER_Y = 12.0;
        public const double OUTER_ELLIPSE_RADIUS_X = 35.0;
        public const double OUTER_ELLIPSE_RADIUS_Y = 10.0;

        private bool InEllipse(
            double x,
            double y,
            double centerX = OUTER_ELLIPSE_CENTER_X,
            double centerY = OUTER_ELLIPSE_CENTER_Y,
            double radiusX = OUTER_ELLIPSE_RADIUS_X,
            double radiusY = OUTER_ELLIPSE_RADIUS_Y
        )
        {
            return
                Math.Pow(x - centerX, 2.0) / Math.Pow(radiusX, 2.0) +
                Math.Pow(y - centerY, 2.0) / Math.Pow(radiusY, 2.0)
            <= 1.0;
        }

        public bool BuildZone(Zone zone)
        {
            var RandomSource = XRL.Rules.Stat.GetSeededRandomGenerator(
                Seed: $"helado_Mycopei_FungalCave_{zone.ZoneID}"
            );

            foreach (var cell in zone.LoopCells())
            {
                var x = cell.X;
                var y = cell.Y;

                cell.AddObject(Blueprint:
                    InEllipse(x, y) ? INNER_BLUEPRINT
                                    : OUTER_BLUEPRINT
                );
            }

            var ellipsesToPlace = RandomSource.Next(5, 11);

            while (ellipsesToPlace > 0)
            {
                var centerX = RandomSource.Next(4, 80 - 4);
                var centerY = RandomSource.Next(4, 25 - 4);
                var radiusX = RandomSource.Next(5, 11);
                var radiusY = RandomSource.Next(5, 11);

                foreach (var cell in zone.LoopCells())
                {
                    var x = cell.X;
                    var y = cell.Y;

                    if (
                        InEllipse(x, y) &&
                        InEllipse(x, y, centerX, centerY, radiusX, radiusY)
                    )
                    {
                        cell.Clear();
                    }
                }

                ellipsesToPlace--;

                if (ellipsesToPlace == 0) {
                    zone.GetCell(centerX, centerY).AddObject("StairsDown");
                }
            }

            return true;
        }
    }
}
