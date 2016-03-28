using System;


public class MovingDerivative
{
	int capacity;
	double sampleSum;
	double timeSum;

	int[] samples;
	double[] times;
	int nextIndex;

	public MovingDerivative (int capacity = 100)
	{
		this.capacity = capacity;
		samples = new int[capacity];
		times = new double[capacity];
	}

	public void AddSample(int sample, double time) {
		sampleSum -= samples[nextIndex];
		timeSum -= times[nextIndex];

		samples[nextIndex] = sample;
		times[nextIndex] = time;

		sampleSum += sample;
		timeSum += time;

		nextIndex = (nextIndex + 1) % capacity;
	}

	// TODO: average weighted down by 0's for first capacity run

	public double GetDerivative() {
		if (timeSum == 0)
			return 0;
		
		return sampleSum / timeSum;
	}
}

