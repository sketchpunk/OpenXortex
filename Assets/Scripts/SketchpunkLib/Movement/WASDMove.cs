using UnityEngine;
using System.Collections;
namespace SP.Movement{
	public class WASDMove {
		public static void onUpdate(Transform tran,float factor){
			float mx = Input.GetAxis("Horizontal"), my = Input.GetAxis("Vertical");

			if(mx < -0.02f || mx > 0.02) tran.position += ((mx > 0)? Vector3.right : Vector3.left) * factor;
			if(my < -0.02f || my > 0.02) tran.position += ((my > 0)? Vector3.up : Vector3.down) * factor;
		}
	}
}