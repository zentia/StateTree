using System;
using System.Reflection;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	public abstract class SharedVariable
	{
		[SerializeField]
		private bool mIsShared;

		[SerializeField]
		private bool mIsGlobal;

		[SerializeField]
		private string mName;

		[SerializeField]
		private string mPropertyMapping;

		[SerializeField]
		private GameObject mPropertyMappingOwner;

		[SerializeField]
		private bool mNetworkSync;

		public bool IsShared
		{
			get
			{
				return mIsShared;
			}
			set
			{
				mIsShared = value;
			}
		}

		public bool IsGlobal
		{
			get
			{
				return mIsGlobal;
			}
			set
			{
				mIsGlobal = value;
			}
		}

		public string Name
		{
			get
			{
				return mName;
			}
			set
			{
				mName = value;
			}
		}

		public string PropertyMapping
		{
			get
			{
				return mPropertyMapping;
			}
			set
			{
				mPropertyMapping = value;
			}
		}

		public GameObject PropertyMappingOwner
		{
			get
			{
				return mPropertyMappingOwner;
			}
			set
			{
				mPropertyMappingOwner = value;
			}
		}

		public bool NetworkSync
		{
			get
			{
				return mNetworkSync;
			}
			set
			{
				mNetworkSync = value;
			}
		}

		public bool IsNone
		{
			get
			{
				return mIsShared && string.IsNullOrEmpty(mName);
			}
		}

		public void ValueChanged()
		{
		}

		public virtual void InitializePropertyMapping(BehaviorSource behaviorSource)
		{
		}

		public abstract object GetValue();

		public abstract void SetValue(object value);
	}
	public abstract class SharedVariable<T> : SharedVariable
	{
		private Func<T> mGetter;

		private Action<T> mSetter;

		[SerializeField]
		protected T mValue;

		public T Value
		{
			get
			{
				return (mGetter == null) ? mValue : mGetter();
			}
			set
			{
				bool flag = !Equals(Value, value);
				if (mSetter != null)
				{
					mSetter(value);
				}
				else
				{
					mValue = value;
				}
				if (flag)
				{
					ValueChanged();
				}
			}
		}

		public override void InitializePropertyMapping(BehaviorSource behaviorSource)
		{
			if (!Application.isPlaying || !(behaviorSource.Owner.GetObject() is Behavior))
			{
				return;
			}
			if (!string.IsNullOrEmpty(PropertyMapping))
			{
				string[] array = PropertyMapping.Split(new char[]
				{
					'/'
				});
				GameObject gameObject;
				if (!Equals(PropertyMappingOwner, null))
				{
					gameObject = PropertyMappingOwner;
				}
				else
				{
					gameObject = (behaviorSource.Owner.GetObject() as Behavior).gameObject;
				}
				Component component = gameObject.GetComponent(TaskUtility.GetTypeWithinAssembly(array[0]));
				Type type = component.GetType();
				PropertyInfo property = type.GetProperty(array[1]);
				if (property != null)
				{
					MethodInfo methodInfo = property.GetGetMethod();
					if (methodInfo != null)
					{
						this.mGetter = (Func<T>)Delegate.CreateDelegate(typeof(Func<T>), component, methodInfo);
					}
					methodInfo = property.GetSetMethod();
					if (methodInfo != null)
					{
						this.mSetter = (Action<T>)Delegate.CreateDelegate(typeof(Action<T>), component, methodInfo);
					}
				}
			}
		}

		public override object GetValue()
		{
			return this.Value;
		}

		public override void SetValue(object value)
		{
			if (mSetter != null)
			{
				mSetter((T)(value));
			}
			else
			{
				mValue = (T)((object)value);
			}
		}

		public override string ToString()
		{
			string arg_2E_0;
			if (this.Value == null)
			{
				arg_2E_0 = "(null)";
			}
			else
			{
				T value = this.Value;
				arg_2E_0 = value.ToString();
			}
			return arg_2E_0;
		}
	}
}
