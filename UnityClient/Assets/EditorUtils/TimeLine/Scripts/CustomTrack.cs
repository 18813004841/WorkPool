using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

//[TrackColor(0, 0, 0)]//设置轨道颜色
//[TrackClipType(typeof(TestDemoAsset1))]//设置clip类型
[TrackBindingType(typeof(GameObject))] //指定可以使用此track的对象类型
public class CustomTrack : TrackAsset
{

}
