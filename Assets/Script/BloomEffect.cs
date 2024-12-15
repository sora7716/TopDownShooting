using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloomEffect : MonoBehaviour
{
    [SerializeField] private Shader highLumiShader_;
    [SerializeField] private Shader blurShader_;
    [SerializeField] private Shader compoShader_;

    private Material highLumiMat_;//���P�x�ӏ��𒊏o����V�F�[�_�����蓖�Ă�}�e���A��
    private Material blurMat_;//�u���[��������V�F�[�_�����蓖�Ă�}�e���A��
    private Material compoMat_;//�e�N�X�`������������V�F�[�_�����蓖�Ă�}�e���A��

    private void Awake()
    {
        highLumiMat_ = new Material(highLumiShader_);
        blurMat_ = new Material(blurShader_);
        compoMat_ = new Material(compoShader_);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //���P�x�ӏ��𒊏o�������̂��i�[���邽�߂̃e�N�X�`��
        RenderTexture highLumiTex = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
        //���P�x�����ڂ��������̂��i�[���邽�߂̃e�N�X�`��
        RenderTexture highLumiblurTex = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);

        //���P�x�ӏ��𒊏o���AhighLumiTex�Ɋi�[
        Graphics.Blit(source, highLumiTex, highLumiMat_);
        //���P�x�ӏ��Ƀu���[���|����blurTedx�Ɋi�[
        Graphics.Blit(source, highLumiblurTex, blurMat_);

        //�����p�V�F�[�_compoMat�́u_HighLumiTex�v�ϐ��Ɋi�[
        compoMat_.SetTexture("_HighLumiBlurTex", highLumiblurTex);
        Graphics.Blit(source, destination, compoMat_);//���摜��n���āA���炩���ߓn����_HighLumiTex�ƍ������ďo��

        RenderTexture.ReleaseTemporary(highLumiblurTex);
        RenderTexture.ReleaseTemporary(highLumiTex);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
