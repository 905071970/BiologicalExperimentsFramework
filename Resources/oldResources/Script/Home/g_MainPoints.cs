using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class g_MainPoints : MonoBehaviour
{
    /// <summary>
    /// 知识要点文本
    /// </summary>
    public Text[] conterText;

    //文本盒子，存放所有文本，滚动浏览需要移动
    private Transform boxTemp;

    //boxTemp每次移动的距离
    private float lerp;
    

    private void OnEnable()
    {
        boxTemp = transform.Find("boxTemp");
        boxTemp.GetComponent<RectTransform>().localPosition = Vector3.zero;
        lerp = -Screen.height * 0.8f;
        //界面初始化
        for (int i=0;i< boxTemp.childCount;i++)
        {
            boxTemp.GetChild(i).GetComponent<RectTransform>().localPosition = i * Vector3.up*lerp;

            boxTemp.GetChild(i).gameObject.SetActive(true);
        }
    }
    private void Start()
    {
        //小标题的文字大小
        int size = 40;
        #region 文本内容
        conterText[0].text = "<color=#FFFFFF00>----</color>兼性厌氧,营养要求不高。生长繁殖的温度范围广(18~37℃)，耐碱不耐酸，在pH8.8~9.0的碱性蛋白胨水或碱性琼脂平板生长良好，初次分离霍乱弧菌常用碱性蛋白胨水增菌。霍乱弧菌可在无盐环境中生长。 触酶、氧化酶均阳性,能发酵单糖、双糖和醇糖,产酸不产气；不分解阿拉伯糖；还原硝酸盐，吲哚阳性。对弧菌抑制剂O/129敏感。\n<color=#FFFFFF00>----</color>弧菌氧化酶阳性并发酵葡萄糖。根据前一个表型特征可将各种弧菌与肠杆菌科内成员区分,依据后者可与假单胞菌属和其他非发酵革兰阴性杆菌相区别。一旦发现某菌具有发酵葡萄糖且氧化酶阳性的特性,则必须鉴别其属于弧菌(气单胞菌抑或邻单胞菌)。"; conterText[1].text = "<color=#007eff><size=" + size + ">1.检验程序：</size></color>粪便或呕吐物接种碱性蛋白胨水增菌，接种4号琼脂平板，出现黑色菌落，氧化酶阳性，进行霍乱弧菌血清学凝集试验，血清凝集试验阳性(多价与单价)上报疾控部门予以确认复核。\n<color=#007eff><size=" + size + ">2.标本采集和运送：</size></color>霍乱是烈性传染病,凡在流行季节和地区有腹泻症状的患者均应快速准确作出病原学诊断。 在发病早期,尽量在使用抗菌药物之前采集标本。 可取患者“米泔水”样便，亦可采取呕吐物或尸体肠内容物，在腹泻的急性期也可采取肛拭子,标本应避免接触消毒液。采取的标本最好就地接种碱性蛋白胨水增菌，不能及时接种者(转运时间超过1小时)可用棉签挑取标本或将肛拭子直接插入卡-布(Cary-Blair)运送培养基中，而甘油盐水缓冲液不适合弧菌的运送(因甘油对弧菌有毒性)。送检标本装在密封、不易破碎的容器中，置室温由专人输送。"; conterText[2].text = "<color=#007eff><size=" + size + ">3.标本直接检查</size></color>\n" + "<color=#ff0000><size=" + size + ">(1)涂片染色镜检：</size></color>取标本直接涂片2张。 干后用甲醇或乙醇固定,复红染色。 油镜观察有无革兰阴性直或微弯曲的杆菌。" + "\n<color=#ff0000><size=" + size + ">(2)动力和制动试验：</size></color>直接取“米泔水”样便,制成悬滴(或压滴)标本后,在暗视野或相差显微镜下直接观察有无呈特征性快速流星样运动的细菌。 同法重新制备另一标本涂片,在悬液中加入1滴不含防腐剂的霍乱多价诊断血清(效价》1:64)。 可见最初呈快速流星样运动的细菌停止运动并发生凝集,则为制动试验阳性。 可初步推定存在霍乱弧菌。" + "\n<color=#ff0000><size=" + size + ">(3)快速诊断：</size></color>通过直接荧光抗体染色和抗O,群或O139群抗原的单克隆抗体凝集试验,能够快速诊断霍乱弧菌感染。" + "\n<color=#ff0000><size=" + size + ">(4)霍乱毒素的测定：</size></color>粪便标本中霍乱毒素(CT)可采用ELISA法检测,或采用商品化的乳胶凝集试验测定,有较高的灵敏度和特异性。但我国很少应用。"; conterText[3].text = "<color=#007eff><size=" + size + ">4.分离培养和鉴定</size></color>\n" + "<color=#FFFFFF00>----</color>将标本直接接种于碱性蛋白胨水(pH8.4),或将运送培养基的表层接种于碱性蛋白胨水中,35&apos;C5~8小时后,转种TCBS琼脂、4号琼脂或庆大霉素琼脂平板,35℃18~24小时观察菌落形态。在TCBS琼脂上形成黄色菌落(分解蔗糖产酸),4号琼脂或庆大霉素琼脂平板上呈灰黑色中心的菌落(还原培养基中的碲离于为灰黑色的金属碲),均为可疑菌落。 应使用O,群和O139群霍乱弧菌多价和单价抗血清进徉凝集。 结合菌落特征和菌体形态，作出初步报告。\n将血清凝集确定的菌落进一步纯培养)依据全面生化反应、血清学分群及分型进行最后鉴定。"; conterText[4].text = "<color=#007eff><size=" + size + ">4.分离培养和鉴定-</size></color>霍乱弧菌的签定试验\n<color=#ff0000><size=" + size + ">(1)霍乱红试验:</size></color>霍乱弧菌有色氨酸酶和硝酸盐还原能力。当将霍乱弧菌培养于含硝酸盐的蛋白胨水中时,可分解培养基中的色氨酸产生吲哚。同时,还原硝酸盐成为亚硝酸盐,两种产物结合成亚硝酸吲哚。滴加浓硫酸后呈现蔷薇色,是为霍乱红试验阳性。 霍乱弧菌和其他弧菌均有此种反应。\n<color=#ff0000><size=" + size + ">(2)黏丝试验:</size></color>将0.5%去氧胆酸钠水溶液与霍乱弧菌混匀成浓悬液。 1分钟内悬液由混变清,并变黏稠,以接种环挑取时有黏丝形成。 弧菌属细菌除副溶血性弧菌部分菌株外,均有此反应。"; conterText[5].text = "<color=#007eff><size=" + size + ">4.分离培养和鉴定-</size></color>霍乱弧菌的签定试验" + "\n<color=#ff0000><size=" + size + ">(3)/129敏感试验：</size></color>O群和非O群霍乱弧菌对O/129(2,4-diamino-6,7-diisopropylp-teridine,2,4二氨基-6,7-二异丙基蝶啶)10ug及150ug的纸片敏感。但已有对O/129耐药的菌株出现。用此试验作鉴定时需特别谨慎。应结合其他试验结果,如耐盐生长试验等综合考虑。" + "\n<color=#ff0000><size=" + size + ">(4).耐盐培养试验：</size></color>霍乱弧菌能在不含氯化钠和含3%氯化钠培养基中生长。氯化钠浓度高于6%则不生长。鸡红细胞凝集试验、多黏菌素B敏感试验和第IN、V组噬菌体裂解试验等用于区别古典和E1-Tor生物型。";
        #endregion
    }
    private void Update()
    {

        ////激活操作
        //if(Input.GetMouseButtonDown(0))
        //{
        //    Netx();
        //}
        //if (Input.GetMouseButtonDown(1))
        //    Back();
        if(Input.GetAxis("Mouse ScrollWheel")>0)
        {
            Back();
        }
        if(Input.GetAxis("Mouse ScrollWheel")<0)
        {
            Next();
        }

    }

    /// <summary>
    /// 滚动下一页
    /// </summary>
    public void Next()
    {
        if ((boxTemp.localPosition.y / lerp) <=-5)
            return;
        StopAllCoroutines();        //关闭all协程，打开roll协程。从当前位置插值运动到目标位置
        StartCoroutine(Roll(boxTemp.GetComponent<RectTransform>(), boxTemp.localPosition, (int)((boxTemp.localPosition.y / lerp) - 1) * lerp * Vector3.up));
    }

    /// <summary>
    /// 滚动上一页
    /// </summary>
    public void Back()
    {
        if ((boxTemp.localPosition.y / lerp) >= 0)
            return;
        StopAllCoroutines();        //关闭all协程，打开roll协程。从当前位置插值运动到目标位置
        StopAllCoroutines(); StartCoroutine(Roll(boxTemp.GetComponent<RectTransform>(), boxTemp.localPosition, (int)((boxTemp.localPosition.y / lerp) + 1) * lerp * Vector3.up));
    }



    /// <summary>
    /// 滚动协程，知识要点box，初始位置，最终位置
    /// </summary>
    /// <param name="box"></param>
    /// <param name="startPoint"></param>
    /// <param name="endPoint"></param>
    /// <returns></returns>
    private IEnumerator Roll(RectTransform box, Vector3 startPoint, Vector3 endPoint)
    {
        float f = 0;
        while(f<1)
        {
            f += 0.1f;
            box.localPosition = Vector3.Lerp(startPoint, endPoint, f);
            yield return new WaitForSeconds(0.02f);
        }
        box.localPosition = endPoint;
        yield return null;
    }
    
}
