using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeControl : MonoBehaviour
{
    //�t�F�[�h�A�E�g�̊J�n�t���O
    bool isFadeOut_ = false;
    //�t�F�[�h�C���̊J�n�t���O
    bool isFadeIn_ = false;
    //�t���[��
    float frame_ = 0.0f;
    //���b��ɏI��点��
    [SerializeField] float endSecond_ = 2.0f;
    //image
    Image image_;
    //�I���t���O
    bool isFinished_ = false;
    // Start is called before the first frame update
    void Start()
    {
        image_ = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// �t�F�[�h�A�E�g
    /// </summary>
    /// <param name="beginColor">�ŏ��̃J���[</param>
    /// <param name="endColor">�Ō�̃J���[</param>
    public void FadeOut(Color beginColor, Color endColor)
    {
        if (isFadeOut_)
        {
            if (frame_ < endSecond_)
            {
                frame_ += Time.deltaTime / endSecond_;
                image_.color = Vector4.Lerp(beginColor, endColor, frame_);
            }
            else
            {
                isFadeOut_ = false;
                isFinished_ = true;
                frame_ = 0.0f;
            }
        }
    }

    public void FadeIn(Color beginColor, Color endColor)
    {
        if (isFadeIn_)
        {
            if (frame_ < endSecond_)
            {
                frame_ += Time.deltaTime / endSecond_;
                image_.color = Vector4.Lerp(beginColor, endColor, frame_);
            }
            else
            {
                isFadeIn_ = false;
                isFinished_ = true;
                frame_ = 0.0f;
            }
        }
    }

    /// <summary>
    /// �t�F�[�h�A�E�g�̃t���O�̃Z�b�^�[
    /// </summary>
    /// <param name="isFadeOut"></param>
    public void SetIsFadeOut(bool isFadeOut)
    {
        isFadeOut_ = isFadeOut;
    }

    /// <summary>
    /// �t�F�[�h�C���̃t���O�̃Z�b�^�[
    /// </summary>
    /// <param name="isFadeIn"></param>
    public void SetIsFadeIn(bool isFadeIn)
    {
        isFadeIn_ = isFadeIn;
    }
    /// <summary>
    /// �I���t���O
    /// </summary>
    /// <returns></returns>
    public bool isFinished()
    {
        return isFinished_;
    }

    /// <summary>
    /// �����ɂ���(�����ɂ����l��Ԃ�)
    /// </summary>
    /// <param name="color">�D���ȐF</param>
    public Color Initialize(Color color)
    {
        color.a = 0.0f;
        image_.color = color;//�C���[�W�𓧖��ɂ���
        return color;
    }

    /// <summary>
    /// �t�F�[�h���I��鎞�ԕύX(�b)
    /// </summary>
    /// <param name="endSecond">�I��鎞��</param>
    public void SetEndSecond(float endSecond)
    {
        endSecond_ = endSecond;
    }

    /// <summary>
    /// �t�F�[�h�����t���O���ւ��܂�
    /// </summary>
    /// <param name="isFinished">�����t���O</param>
    public void SetIsFinished(bool isFinished)
    {
        isFinished_ = isFinished;
    }
}
