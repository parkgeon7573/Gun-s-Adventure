//=========================================================================
/*
 * [ ���� : https://blog.naver.com/jerrypoiu/221235988023
 *			http://www.ktword.co.kr/word/abbr_view.php?m_temp1=3203
 *			https://boycoding.tistory.com/262
 *			https://blog.naver.com/debuff9710/221333379009 ]
 *			 
 *	------------------------
 *	���� ���� �ӽ�...
 *	------------------------
 *		
 *		-----------
 *		-	����..
 *		-----------
 *		
 *			-	���Ѱ��� ���¸� ������ �־����� �Է¿� ����
 *				� ���¿��� �ٸ� ���·� ��ȯ�ϰų�
 *				����̳� �׼��� �Ͼ�� �ϴ� �ý���..
 *				
 *
 *		---------------
 *		-	�⺻ ����..
 *		---------------
 *			
 *			-	[ ���� ]		:	���� �������� �����̳� ���..
 *			-	[ ���� ]		:	���� [ ���� ]���� �ٸ� [ ���� ]�� �ٲ�� ��..
 *			-	[ �̺�Ʈ ]	:	[ ���� ]�� ���� ����..
 *	
 *	
 *	
 *		------------
 *		-	Ư¡..
 *		------------
 *		
 *			-	[ ���� ]�� ������ �����ϴ�..
 *			-	�ѹ��� �ϳ��� ���¸� ���� ����..
 *			
 *			
 *		------------
 *		-	�����
 *		------------
 *		
 *			------------
 *			-	����..
 *			------------
 *			
 *				-	���� ������ ����..
 *					-	�� ���°� ���ȭ �Ǿ������Ƿ� ������ ������ ã�� ����..
 *					
 *				-	������..
 *					-	���µ��� ���������� �����Ǿ� �����Ƿ� 
 *						[ ���� ]�� [ �̺�Ʈ ]�� �ľ��ϱ� ����..
 *						
 *				-	������..
 *					-	���ο� ���¸� �߰��ϰų� [ ���� ]���踦 �����ϴ� ���� ����..
 *					
 *					
 *			------------
 *			-	����..
 *			------------
 *				
 *				-	[ ���� ]�� �Ը� Ŀ���� ���谡 ��������..
 *				-	���ѵ� ������ �������� ���� ����..
 *				-	�ܼ� ��� �۾����� ������..
 *				-	������ �ܼ��Ͽ� �ൿ�� ��� ������ ����..
 *				 
 */
//=========================================================================
using UnityEngine;
//=========================================================================
//	FSM ( Finite State Machine.. )
public class FSM <T>  : MonoBehaviour
{
	//---------------------------------------
	private T owner;	//	���� ������..
	private IFSMState<T> currentState = null;	//	���� ����..
	private IFSMState<T> previousState = null;	//	���� ����..
	//---------------------------------------
	public IFSMState<T> CurrentState{ get {return currentState;} }
	public IFSMState<T> PreviousState{ get {return previousState;} }
	//---------------------------------------
	//	�ʱ� ���¿� ���� ������ ����..
	protected void InitState(T owner, IFSMState<T> initialState)
	{
		this.owner = owner;
		ChangeState(initialState);
	}
	//---------------------------------------
	//	�� ������ �ǽð� ó��..
	protected void  FSMUpdate() { if (currentState != null) currentState.Execute(owner); }
	//--------------------------------------- 
	//	���� ����..
	public void  ChangeState(IFSMState<T> newState)
	{
		//	���� ���� ��ü..
		previousState = currentState;
 
		//	���� ���� ����!!
		if (previousState != null)
			previousState.Exit(owner);
 
		//	���� ���� ��ü..
		currentState = newState;

		//	���� ���� ����!!
		if (currentState != null)
			currentState.Enter(owner);
	
	}//	public void  ChangeState(IState<T> newState)
	//--------------------------------------- 
	//	���� ���·� ��ȯ..
	public void  RevertState()
	{
		if (previousState != null)
			ChangeState(previousState);
	
	}//	public void  RevertState()
	//---------------------------------------
	//	������...
	//	-	������� Ȯ��..
	public override string ToString() { return currentState.ToString(); }
	//---------------------------------------

}//	public class FSM <T>  : MonoBehaviour
//=========================================================================