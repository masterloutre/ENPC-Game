using System;

public interface ValidationMethod
{
	bool answerIsRight();
	Score fillScore(Score score);
	string getScoreFeedback();
}
