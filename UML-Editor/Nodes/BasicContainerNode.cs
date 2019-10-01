using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.EventArguments;
using UML_Editor.Geometry;
using UML_Editor.NodeStructure;
using UML_Editor.Rendering;
using UML_Editor.Rendering.ElementStyles;

namespace UML_Editor.Nodes
{
    public class BasicContainerNode : BasicNode, IContainerNode
    {
        public override Vector Position
        {
            get => base.Position;
            set
            {
                base.Position = value;
                RepositionChildren();
            }

        }

        public BasicContainerNode(BasicNodeStructure structure, RectangleRenderElementStyle border_style) : base(structure, border_style)
        {
        }

        public virtual void RepositionChildren()
        {
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].Position = new Vector(Position.X, Position.Y + i * Renderer.SingleTextHeight);
            }
        }

        public virtual void AddNode(INode node)
        {
            Children.Add(node);
            if (node is IFocusableNode fn)
            {
                fn.OnFocused += OnNodeFocus;
                fn.OnUnfocused += OnNodeUnfocus;
            }
            node.OnResize += OnChildResize;
            RepositionChildren();
            OnNodeAdd?.Invoke(this, new NodeEventArgs(node));
        }
        public virtual void RemoveNode(INode node)
        {
            if(Children.Contains(node))
            {
                Children.Remove(node);
                RepositionChildren();
                OnNodeRemoval?.Invoke(this, new NodeEventArgs(node));
            }
        }
        public virtual void RemoveNode(int index)
        {
            if (index >= 0 && index < Children.Count)
            {
                INode removed = Children[index];
                Children.RemoveAt(index);
                OnNodeRemoval?.Invoke(this, new NodeEventArgs(removed));
            }
        }
        public virtual void OnNodeFocus(object sender, NodeEventArgs e)
        {
            if (FocusedNode != e.Node)
            {
                FocusedNode?.OnUnfocused?.Invoke(this, new NodeEventArgs(FocusedNode));
                FocusedNode = (IFocusableNode) e.Node;
            }
        }
        public virtual void OnNodeUnfocus(object sender, NodeEventArgs e)
        {
            if (FocusedNode == e.Node)
                FocusedNode = null;
        }

        public virtual void OnChildResize(object sender, ResizeEventArgs e)
        {
            RepositionChildren();
        }

        public override void Render(Renderer renderer)
        {
            base.Render(renderer);
            Children.OfType<IRenderableNode>().ToList().ForEach(x => x.Render(renderer));
            FocusedNode?.Render(renderer);
        }

        public List<IFocusableNode> GetFocusableNodes() => Children.OfType<IFocusableNode>().ToList();
        public List<INode> Children { get; set; } = new List<INode>();
        public IFocusableNode FocusedNode { get; set; }
        public EventHandler<NodeEventArgs> OnNodeAdd { get; set; }
        public EventHandler<NodeEventArgs> OnNodeRemoval { get; set; }
    }
}
