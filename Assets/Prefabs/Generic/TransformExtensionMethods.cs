using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensionMethods
{
	public static void ResetTransformation(this Transform trans)
	{
		trans.position = Vector3.zero;
		trans.localRotation = Quaternion.identity;
		trans.localScale = new Vector3(1, 1, 1);
	}

	public static void CopyTransform(this Transform trans, Transform target) {
		target.position = trans.position;
		target.rotation = trans.rotation;
	}


	public static TransformInfo GetAROffsetFromReference(this Transform positionedObject, Transform referenceTransform)
	{
		Transform imageTarget = referenceTransform;
		Transform mediaTransform = positionedObject;

		TransformInfo ti = new TransformInfo();

		//ti.position = mediaTransform.position - imageTarget.position;
		ti.position = referenceTransform.InverseTransformPoint(positionedObject.position);

		Quaternion relativeRotation = Quaternion.Inverse(imageTarget.rotation) * mediaTransform.rotation;
		ti.rotation = relativeRotation;
		ti.scale = mediaTransform.localScale;

		return ti;
	}

	public static void SetAROffset(this Transform positionedObject, Transform referenceTransform,  TransformInfo relativeOffset)
	{
		TransformInfo ti = new TransformInfo();
		ti.position = relativeOffset.position;
		ti.rotation = relativeOffset.rotation;
		ti.scale = relativeOffset.scale;
		
		TransformInfo absoluteTransform = ti;
		absoluteTransform.position = referenceTransform.TransformPoint(relativeOffset.position);

		Quaternion relativeRotation = relativeOffset.rotation;
		Quaternion sculptureTransformRotation = referenceTransform.rotation * relativeRotation;

		//relativeOffset.rotation = sculptureTransformRotation;

		positionedObject.rotation = sculptureTransformRotation;
		positionedObject.position = absoluteTransform.position;
		positionedObject.localScale = absoluteTransform.scale;

		//positionedObject.rotation = offset.rotation;

		// Add it to like a parent temporarily and then detach it

	}
}
