using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleVerifier : MonoBehaviour
{
    public System.Func<List<int>, List<List<int>>> MakeHorizonAnswerListDelegate;
    public System.Func<List<int>, List<List<int>>> MakeVerticalAnswerListDelegate;
    public bool CheckUserAnswer(List<int> userAnswer, List<List<HintUIText>> HorizonHintUITextList, List<List<HintUIText>> VerticalHintUITextList)
    {
        ResetHintUITextColor(HorizonHintUITextList, VerticalHintUITextList); // 리셋 한번하고 다시 판단하여 텍스트 컬러 변경

        List<List<int>> InnerHorizonUserAnswerList = MakeHorizonAnswerListDelegate.Invoke(userAnswer);
        List<List<int>> InnerVerticalUserAnswerList = MakeVerticalAnswerListDelegate.Invoke(userAnswer);

        for (int i = 0; i < InnerHorizonUserAnswerList.Count; i++)
        {
            for (int j = 0; j< InnerHorizonUserAnswerList[i].Count; j++)
            {
                for (int k = 0; k < HorizonHintUITextList[i].Count; k++)
                {
                    if(InnerHorizonUserAnswerList[i][j] == HorizonHintUITextList[i][k].GetTextValue())
                    {
                        HorizonHintUITextList[i][k].ChangeState(true);
                        continue;
                    }
                }
            }

            for (int j = 0; j < InnerVerticalUserAnswerList[i].Count; j++)
            {
                for (int k = 0; k < VerticalHintUITextList[i].Count; k++)
                {
                    if (InnerVerticalUserAnswerList[i][j] == VerticalHintUITextList[i][k].GetTextValue())
                    {
                        VerticalHintUITextList[i][k].ChangeState(true);
                        continue;
                    }
                }    
            }
        }

        return VerifyAllAnswers(HorizonHintUITextList, VerticalHintUITextList);
    }

    public bool VerifyAllAnswers(List<List<HintUIText>> HorizonHintUITextList, List<List<HintUIText>> VerticalHintUITextList)
    {
        bool correctAllAnswers = true;

        for(int i = 0; i < HorizonHintUITextList.Count; i++)
        {
            for(int j = 0; j < HorizonHintUITextList[i].Count; j++)
            {
                if (HorizonHintUITextList[i][j].GetIsCorrect() == false)
                {
                    correctAllAnswers = false;
                    return correctAllAnswers;
                }
            }

            for (int j = 0; j < VerticalHintUITextList[i].Count; j++)
            {
                if (VerticalHintUITextList[i][j].GetIsCorrect() == false)
                {
                    correctAllAnswers = false;
                    return correctAllAnswers;
                }
            }
        }

        return correctAllAnswers;
    }

    public void ResetHintUITextColor(List<List<HintUIText>> HorizonHintUITextList, List<List<HintUIText>> VerticalHintUITextList)
    {
        for(int i = 0; i< HorizonHintUITextList.Count; i++)
        {
            for(int j = 0; j < HorizonHintUITextList[i].Count; j++)
            {
                HorizonHintUITextList[i][j].ChangeState(false);
            }

            for (int j = 0; j < VerticalHintUITextList[i].Count; j++)
            {
                VerticalHintUITextList[i][j].ChangeState(false);
            }
        }
    }
}
