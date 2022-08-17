public class TalentGraph : Graph<Talent>
{
    public override void ResetState()
    {
        throw new System.NotImplementedException();
    }

    public void Foo(Node node)
    {
        if (node.Nodes.Count < 1) return;

        foreach (var linkedNode in node.Nodes)
        {

        }
    }
}
