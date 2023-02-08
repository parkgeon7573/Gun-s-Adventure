/// <summary>
/// Update를 위임하기 위한 인터페이스. Update를 사용하는 모든 오브젝트에 붙이면 된다
/// </summary>
public interface IUpdateableObject
{

    void OnUpdate();
}

