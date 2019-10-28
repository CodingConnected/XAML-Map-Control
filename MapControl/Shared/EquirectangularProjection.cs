﻿// XAML Map Control - https://github.com/ClemensFischer/XAML-Map-Control
// © 2019 Clemens Fischer
// Licensed under the Microsoft Public License (Ms-PL)

using System;
#if !WINDOWS_UWP
using System.Windows;
#endif

namespace MapControl
{
    /// <summary>
    /// Equirectangular Projection.
    /// Longitude and Latitude values are transformed identically to X and Y.
    /// </summary>
    public class EquirectangularProjection : MapProjection
    {
        public EquirectangularProjection()
            : this("EPSG:4326")
        {
        }

        public EquirectangularProjection(string crsId)
        {
            CrsId = crsId;
            HasLatLonBoundingBox = CrsId != "CRS:84";
            IsNormalCylindrical = true;
            TrueScale = 1d;
        }

        public override Vector GetMapScale(Location location)
        {
            return new Vector(
                ViewportScale / (Wgs84MetersPerDegree * Math.Cos(location.Latitude * Math.PI / 180d)),
                ViewportScale / Wgs84MetersPerDegree);
        }

        public override Point LocationToPoint(Location location)
        {
            return new Point(location.Longitude, location.Latitude);
        }

        public override Location PointToLocation(Point point)
        {
            return new Location(point.Y, point.X);
        }
    }
}
