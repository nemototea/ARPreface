using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class PutScript : MonoBehaviour
{
	public GameObject mTestModel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Touch touch;

		touch = Input.GetTouch(0);

		if (Input.touchCount < 1 ||
			(touch.phase != TouchPhase.Moved))
		{
			return; // スワイプしているのでなければ何もしない
		}

		// 平面検出したポリゴンの内側を、タップ判定の対象にする
		TrackableHit hit;
		TrackableHitFlags filter = TrackableHitFlags.PlaneWithinPolygon;

		if (Frame.Raycast(touch.position.x, touch.position.y, filter, out hit))
		{
			// 平面にヒットしたなら
			if (hit.Trackable is DetectedPlane)
			{
				// 位置・姿勢を指定
				mTestModel.transform.position = hit.Pose.position;
				mTestModel.transform.rotation = hit.Pose.rotation;
				mTestModel.transform.Rotate(0, 180, 0, Space.Self);

				// Anchorを設定
				var anchor = hit.Trackable.CreateAnchor(hit.Pose);
				mTestModel.transform.parent = anchor.transform;
			}
		}
    }
}
