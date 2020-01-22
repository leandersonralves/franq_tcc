public enum Feedback {
	NoDiegetic,
	Diegetic
}

public interface IFeedback {

	Feedback Feedback {
		get;
	}

	void Enable ();
	void Disable ();
}