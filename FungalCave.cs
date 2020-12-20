using System;

namespace XRL.World.ZoneBuilders
{
    public class helado_Mycopei_FungalCave
    {
        private bool InEllipse(double x, double y, double centerX, double centerY, double radiusX, double radiusY) {
            return
                Math.Pow(x - centerX, 2.0) / Math.Pow(radiusX, 2.0) +
                Math.Pow(y - centerY, 2.0) / Math.Pow(radiusY, 2.0)
            <= 1.0;
        }

        public bool BuildZone(Zone zone)
        {
            var RandomSource = XRL.Rules.Stat.GetSeededRandomGenerator(
                Seed: $"helado_Mycopei_FungalCave {zone.ZoneID}"
            );

            zone.Fill(Blueprint: "helado_Mycopei_Fungal Wall");
            var ovalsToPlace = 7;

            while (ovalsToPlace > 0) {
                var centerX = RandomSource.Next(8, 80 - 8);
                var centerY = RandomSource.Next(8, 25 - 8);
                var radiusX = RandomSource.Next(2, 7);
                var radiusY = RandomSource.Next(2, 7);

                foreach (var cell in zone.LoopCells()) {
                    if (InEllipse(cell.X, cell.Y, centerX, centerY, radiusX, radiusY)) {
                        cell.Clear();
                    }
                }

                ovalsToPlace--;
            }

            return true;
        }
    }
}
