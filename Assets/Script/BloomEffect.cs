using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloomEffect : MonoBehaviour
{
    [SerializeField] private Shader highLumiShader_;
    [SerializeField] private Shader blurShader_;
    [SerializeField] private Shader compoShader_;

    private Material highLumiMat_;//高輝度箇所を抽出するシェーダを割り当てるマテリアル
    private Material blurMat_;//ブラーをかけるシェーダを割り当てるマテリアル
    private Material compoMat_;//テクスチャを合成するシェーダを割り当てるマテリアル

    private void Awake()
    {
        highLumiMat_ = new Material(highLumiShader_);
        blurMat_ = new Material(blurShader_);
        compoMat_ = new Material(compoShader_);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //高輝度箇所を抽出したものを格納するためのテクスチャ
        RenderTexture highLumiTex = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
        //高輝度個所をぼかしたものを格納するためのテクスチャ
        RenderTexture highLumiblurTex = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);

        //高輝度箇所を抽出し、highLumiTexに格納
        Graphics.Blit(source, highLumiTex, highLumiMat_);
        //高輝度箇所にブラーを掛けてblurTedxに格納
        Graphics.Blit(source, highLumiblurTex, blurMat_);

        //合成用シェーダcompoMatの「_HighLumiTex」変数に格納
        compoMat_.SetTexture("_HighLumiBlurTex", highLumiblurTex);
        Graphics.Blit(source, destination, compoMat_);//元画像を渡して、あらかじめ渡した_HighLumiTexと合成して出力

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
