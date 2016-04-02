using System;


public class MovingAverage
{
	int capacity;
	double sampleSum;

	double[] samples;
	int nextIndex;

	public MovingAverage (int capacity = 100)
	{
		this.capacity = capacity;
		samples = new double[capacity];
	}

	public void AddSample(double sample) {
		sampleSum -= samples[nextIndex];
		samples[nextIndex] = sample;
		sampleSum += sample;

		nextIndex = (nextIndex + 1) % capacity;
	}

	// TODO: average weighted down by 0's for first capacity run

	public double GetDerivative() {
		if (sampleSum == 0)
			return 0;

		return sampleSum / capacity;
	}

	public double SmoothValue(double sample) {
		AddSample(sample);
		return GetDerivative();
	}
}

