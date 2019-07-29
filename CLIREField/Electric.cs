using System;

/// <summary>
/// The TestCell namespace encapsulates classes that approximate electric field, water production,
/// and consolidation in the EKS Test Cell.
/// </summary>
namespace TestCell
{
    public class Electric
    {
        public double relativePermittivityFreeSpace = 8.854 * Math.Pow(10.0, -12.0);

        /// <summary>
        /// k is the common multiplier in volts for voltage and electric field equations.
        /// </summary>
        /// <param name="chargeDensity">Charge Density in [C/m]</param>
        /// <param name="relativePermittivity">Average relative permittivity of the FFT.</param>
        /// <returns>the k Multiplier in volts [V].</returns>
        public double k(double chargeDensity, double relativePermittivity)
        {
            return chargeDensity / (4 * Math.PI * relativePermittivityFreeSpace * relativePermittivity );
        }

     /// <summary>
     /// Voltage returns the voltage at the (x, z) location due to an anode at coordinates (xPair, zAnode)
     /// and a cathode at coordinates (xPair, zCathode).
     /// </summary>
     /// <param name="x">The x-coordinate of the voltage probe location in metres [m].</param>
     /// <param name="z">The z-coordinate of the voltage probe location in metres [m].</param>
     /// <param name="xPair">The x-coordinate of the anode - cathode pair in metres [m].</param>
     /// <param name="zAnode">The z-coordinate of the anode in metres [m].</param>
     /// <param name="zCathode">The z-coordinate of the cathode in metres [m].</param>
     /// <param name="k_">k-multiplier in volts [V].</param>
     /// <returns>Voltage [V] at (x,z) due to the anode at (xPair, zAnode) and cathode at (xPair, zCathode).</returns>
        public double Voltage(double x, double z, double xPair, double zAnode, double zCathode, double k_)
        {
            double distanceFactor = (Math.Pow(x - xPair, 2.0) + Math.Pow(z - zCathode, 2.0)) /
                                    (Math.Pow(x - xPair, 2.0) + Math.Pow(z - zAnode, 2.0));
            return k_ * Math.Log(distanceFactor);
        }

        /// <summary>
        /// Ex returns the magnitude of the x-component of the electric field at location (x, z) 
        /// due to an anode at (xPair, zAnode) and a cathode at (xPair, zCathode).
        /// </summary>
        /// <param name="x">The x-coordinate of the voltage probe location in metres [m].</param>
        /// <param name="z">The z-coordinate of the voltage probe location in metres [m].</param>
        /// <param name="xPair">The x-coordinate of the anode - cathode pair in metres [m].</param>
        /// <param name="zAnode">The z-coordinate of the anode in metres [m].</param>
        /// <param name="zCathode">The z-coordinate of the cathode in metres [m].</param>
        /// <param name="k_">k-multiplier in volts [V].</param>
        public double Ex(double x, double z, double xPair, double zAnode, double zCathode, double k_)
        {
            return 2.0 * k_ * (1 / (Math.Pow(x - xPair, 2.0) - Math.Pow(zAnode - z, 2.0)) -
                               1 / (Math.Pow(x - xPair, 2.0) - Math.Pow(z - zCathode, 2.0)));
        }

        /// <summary>
        /// Ez returns the magnitude of the z-component of the electric field at location (x, z) 
        /// due to an anode at (xPair, zAnode) and a cathode at (xPair, zCathode).
        /// </summary>
        /// <param name="x">The x-coordinate of the voltage probe location in metres [m].</param>
        /// <param name="z">The z-coordinate of the voltage probe location in metres [m].</param>
        /// <param name="xPair">The x-coordinate of the anode - cathode pair in metres [m].</param>
        /// <param name="zAnode">The z-coordinate of the anode in metres [m].</param>
        /// <param name="zCathode">The z-coordinate of the cathode in metres [m].</param>
        /// <param name="k_">k-multiplier in volts [V].</param>
        public double Ez(double x, double z, double xPair, double zAnode, double zCathode, double k_)
        {
            return 2.0 * k_ * ((z-zAnode) / (Math.Pow(x - xPair, 2.0) - Math.Pow(zAnode - z, 2.0)) -
                               (zCathode-z) / (Math.Pow(x - xPair, 2.0) - Math.Pow(z - zCathode, 2.0)));
        }


        /// <summary>
        /// EMag returns the magnitude of the electric field at location (x, z) 
        /// due to an anode at (xPair, zAnode) and a cathode at (xPair, zCathode).
        /// </summary>
        /// <param name="x">The x-coordinate of the voltage probe location in metres [m].</param>
        /// <param name="z">The z-coordinate of the voltage probe location in metres [m].</param>
        /// <param name="xPair">The x-coordinate of the anode - cathode pair in metres [m].</param>
        /// <param name="zAnode">The z-coordinate of the anode in metres [m].</param>
        /// <param name="zCathode">The z-coordinate of the cathode in metres [m].</param>
        /// <param name="k_">k-multiplier in volts [V].</param>
        public double EMag(double x, double z, double xPair, double zAnode, double zCathode, double k_)
        {
            return Math.Sqrt(Math.Pow(Ex(x, z, xPair, zAnode, zCathode, k_), 2.0) +
                              Math.Pow(Ez(x, z, xPair, zAnode, zCathode, k_), 2.0));
        }

    }
}
