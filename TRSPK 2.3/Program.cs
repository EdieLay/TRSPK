Node a = new Node("a");
a.AddChildren("b", "j");
Node b = a.GetChildren().GetList()[0];
b.AddChildren("c", "e", "g");
Node j = a.GetChildren().GetList()[1];
j.AddChildren("k", "m");
Node c = b.GetChildren().GetList()[0];
c.AddChildren("d");
Node e = b.GetChildren().GetList()[1];
e.AddChildren("f");
Node g = b.GetChildren().GetList()[2];
g.AddChildren("h", "i");
Node k = j.GetChildren().GetList()[0];
k.AddChildren("l");
string result = System.String.Empty;
Console.WriteLine(a.BuildTree(ref result, "", 0));

public class Node
{
    NodeList Children;
    string Text;

    public Node()
    {
        Text = "";
        Children = new NodeList();
    }
    public Node(string text, NodeList children)
    {
        Text = text;
        Children = children;
    }
    public Node(string text)
    {
        Text= text;
        Children = new NodeList();
    }

    public void AddChildren(params string[] children) // добавление нескольких потомков по тексту
    {
        for (int i = 0; i < children.Length; i++)
        {
            Node child = new Node(children[i]);
            Children.Add(child);
        }
    }

    public NodeList GetChildren()
    {
        return Children;
    }
    public string GetText()
    {
        return Text;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="result"></param>
    /// <param name="otstup"></param>
    /// <returns></returns>
    public string BuildTree(ref string result, string otstup, int param)
    {
        switch (param)
        {
            case 0:
                result = result + Text + Environment.NewLine;
                break;
            case 1:
                result = result + otstup + "├─" + Text + Environment.NewLine;
                otstup = otstup + "│ ";
                break;
            case 2:
                result = result + otstup + "└─" + Text + Environment.NewLine;
                otstup = otstup + "  ";
                break;
        }
        List<Node> children = Children.GetList();
        for (int i = 0; i < children.Count - 1; i++)
        {
            children[i].BuildTree(ref result, otstup, 1);
        }
        if (children.Count != 0)
        {
            children[children.Count - 1].BuildTree(ref result, otstup, 2);
        }
        return result;

    }
}
public class NodeList
{
    List<Node> List;

    public NodeList()
    {
        List = new List<Node>();
    }

    public List<Node> GetList()
    {
        return List;
    }

    public void Add(Node New = null) // добавление ноды в конец списка с нодой по умолчанию
    {
        if (New == null)
        {
            New = new Node();
        }
        List.Add(New);
    }

    public Node GetNode(int n) // получение ноды обычное
    {
        if (n >= 0 && n < List.Count)
        {
            return List[n];
        }
        else throw new Exception();
    }

    public void GetNodeOut(int n, out Node node) // получение ноды через out
    {
        if (n >= 0 && n < List.Count)
        {
            node = List[n];
        }
        else throw new Exception();
    }

    public void GetNodeRef(int n, ref Node node) // получение ноды через ref
    {
        if (n >= 0 && n < List.Count)
        {
            node = List[n];
        }
        else throw new Exception();
    }

    public void Add(in Node New) // добавление ноды через in
    {
        List.Add(New);
    }
}
public static class NodeListExtension
{
    public static int NumOfGrandChildren(NodeList nodelist) // количество внуков
    {
        int num = 0;
        List<Node> temp = nodelist.GetList();
        NodeList templist = new NodeList();
        for(int i = 0; i < temp.Count; i++)
        {
            templist = temp[i].GetChildren();
            num += templist.GetList().Count;
        }
        return num;
    }
}