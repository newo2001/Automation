﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Automation.Core
{
    public class Noise {
        double persistence;
        double frequency;
        double amplitude;
        int octaves;
        int randomSeed;

        public Noise(double persistence, double frequency, double amplitude, int octaves, int randomSeed) {
            this.persistence = persistence;
            this.frequency = frequency;
            this.amplitude = amplitude;
            this.octaves = octaves;
            this.randomSeed = randomSeed;
        }

        /// *** \\\
        /// GET \\\
        /// *** \\\

        public double Get2D(double x, double y) {
            double total = 0;
            double tempAmplitude = amplitude;
            double tempFrequency = frequency;
            for (int i = 0; i < octaves; i++) {
                //tempFrequency = Math.Pow(2,i);
                //double tempAmplitude = Math.Pow(persistence,i);
                total += Gradient2D(x * tempFrequency, y * tempFrequency, randomSeed) * tempAmplitude;
                tempAmplitude *= persistence;
                tempFrequency *= 2;
            }

            return total;
        }

        /// ***** \\\
        /// NOISE \\\
        /// ***** \\\

        // Returns a random number based upon the number it is given
        // The big numbers are prime numbers and can be changed to other primes
        public double PerlinNoise1D(int x, int seed) {
            int n = (x + seed) & 0x7fffffff;
            n = (n << 13) ^ n;
            return (1.0 - ((n * (n * n * 15731 + 789221) + 1376312589) & 0x7fffffff) / 1073741824.0);
        }

        public double PerlinNoise2D(int x, int y, int seed) {
            int n = (x + y * seed) & 0x7fffffff;
            n = (n << 13) ^ n;
            return (1.0 - ((n * (n * n * 15731 + 789221) + 1376312589) & 0x7fffffff) / 1073741824.0);
        }

        /// ************** \\\
        /// SEAMLESS NOISE \\\
        /// ************** \\\
        /// NOTE: These functions are WIP

        public double NoiseSeamlessTest2D(int x, int y, int seed) {
            //return NoiseSeamless2D(x, y, seed, 1, 1);
            //return NoiseSeamlessX2D(x, y, seed, 16);
            return 0;
        }

        // Make noise seamless in X and Y direction, wrapping at w and h
        public double NoiseSeamless2D(int x, int y, int seed, int w, int h) {
            double total = 0;
            total += PerlinNoise2D(x, y, seed) * (w - x) * (h - y);
            total += PerlinNoise2D(x - w, y, seed) * (x) * (h - y);
            total += PerlinNoise2D(x - w, y - h, seed) * (x) * (y);
            total += PerlinNoise2D(x, y - h, seed) * (w - x) * (y);
            return total / (w * h);
        }

        // Make noise seamless in just the X direction, wrapping at w
        public double NoiseSeamlessX2D(int x, int y, int seed, int w) {
            return ((w - x) * PerlinNoise2D(x, y, seed) + (x) * PerlinNoise2D(x - w, y, seed)) / w;
        }

        /// ******** \\\
        /// GRADIENT \\\
        /// ******** \\\

        public double Gradient2D(double x, double y, int seed) {
            // Calculate integer coordinates
            int integerX = (x > 0.0 ? (int)x : (int)x - 1);
            int integerY = (y > 0.0 ? (int)y : (int)y - 1);
            // Calculate remainder of coordinates
            double fractionalX = x - (double)integerX;
            double fractionalY = y - (double)integerY;
            // Get values for corners
            double v1 = Smooth2D(integerX, integerY, seed);
            double v2 = Smooth2D(integerX + 1, integerY, seed);
            double v3 = Smooth2D(integerX, integerY + 1, seed);
            double v4 = Smooth2D(integerX + 1, integerY + 1, seed);
            // Interpolate X
            double i1 = InterpolateCosine(v1, v2, fractionalX);
            double i2 = InterpolateCosine(v3, v4, fractionalX);
            // Interpolate Y
            return InterpolateCosine(i1, i2, fractionalY);
        }

        /// ************* \\\
        /// INTERPOLATING \\\
        /// ************* \\\

        // Interpolate linearly between two points, a and b, based upon the value x (which is between 0 and 1)
        // When x is 0, a is returned
        // When x is 1, b is returned
        // Linear interpolation is the fastest but has most jagged output
        public double InterpolateLinear(double a, double b, double x) {
            return a * (1 - x) + b * x;
        }

        // Slightly slower than linear interpolation but much smoother
        public double InterpolateCosine(double a, double b, double x) {
            double ft = x * System.Math.PI;
            double f = (1 - System.Math.Cos(ft)) * 0.5;
            return a * (1 - f) + b * f;
        }

        // Much slower than cosine and linear interpolation, but very smooth
        // v1 = a, v2 = b
        // v0 = point before a, v3 = point after b
        public double InterpolateCubic(double v0, double v1, double v2, double v3, float x) {
            double p = (v3 - v2) - (v0 - v1);
            double q = (v0 - v1) - p;
            double r = v2 - v0;
            double s = v1;
            return p * x * x * x + q * x * x + r * x + s;
        }

        /// ********* \\\
        /// SMOOTHING \\\
        /// ********* \\\

        public double Smooth1D(int x, int seed) {
            // 1/4 + 1/4 + 1/2 = 1;

            return PerlinNoise1D(x, seed) / 2 + PerlinNoise1D(x - 1, seed) / 4 + PerlinNoise1D(x + 1, seed) / 4;
        }

        public double Smooth2D(int x, int y, int seed) {
            // 4/16 + 4/8 + 1/4 = 1

            // -- +- -+ ++
            double corners = (PerlinNoise2D(x - 1, y - 1, seed) + PerlinNoise2D(x + 1, y - 1, seed) +
                              PerlinNoise2D(x - 1, y + 1, seed) + PerlinNoise2D(x + 1, y + 1, seed)) / 16;
            // -0 +0 0- 0+
            double sides = (PerlinNoise2D(x - 1, y, seed) + PerlinNoise2D(x + 1, y, seed) +
                            PerlinNoise2D(x, y - 1, seed) + PerlinNoise2D(x, y + 1, seed)) / 8;
            // 00
            double center = PerlinNoise2D(x, y, seed) / 4;
            return corners + sides + center;
        }
    }
}