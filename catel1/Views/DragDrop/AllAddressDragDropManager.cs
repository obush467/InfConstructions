using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid.DragDrop;
using System.Collections;
using System.Windows.Input;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Utils.MVVM;
using System.Windows.Data;
using System.Windows.Media;

namespace InfConstractions.Views.DragDrop
{
    public class AllAddressDragDropManager : TreeListViewBehavior
    {
        public AllAddressDragDropManager(TreeListView tl) : base(tl)
        {
            TreeListView.DragEnter += TreeListView_DragEnter;        }
        public AllAddressDragDropManager() : base(new TreeListView())
        {
            TreeListView.DragEnter += TreeListView_DragEnter;
        }
        private void TreeListView_DragEnter(object sender, DragEventArgs e)
        {
            throw new NotImplementedException();
        }

        private object draggedItem;
        private bool isEditing;
        private bool isDragging;

        #region DragEnded
        public static readonly RoutedEvent DragEndedEvent =
            EventManager.RegisterRoutedEvent("DragEnded", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(AllAddressDragDropManager));
        
        
        public static void AddDragEndedHandler(DependencyObject d, RoutedEventHandler handler)
        {
            UIElement uie = d as UIElement;
            if (uie != null)
                uie.AddHandler(AllAddressDragDropManager.DragEndedEvent, handler);
        }
        /*protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.AllowDrop = true;
            AssociatedObject.DragEnter += OnDragEnter;
            AssociatedObject.DragLeave += OnDragLeave;
            AssociatedObject.Drop += OnDrop;          
            AssociatedObject.PreviewMouseLeftButtonDown += OnMouseLeftButtonDown;
            AssociatedObject.MouseMove += OnMouseMove;
            
        }*/

        private void rrrrrr (object sender, DevExpress.Xpf.Grid.DragDrop.DragLeaveEventArgs e)
        {
            throw new NotImplementedException();
        }

       /* protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.AllowDrop = false;
            AssociatedObject.DragEnter -= OnDragEnter;
            AssociatedObject.DragLeave -= OnDragLeave;
            AssociatedObject.Drop -= OnDrop;
            AssociatedObject.PreviewMouseLeftButtonDown -= OnMouseLeftButtonDown;
            AssociatedObject.MouseMove -= OnMouseMove;
            draggedItem = null;
            isEditing = false;
            isDragging = false;
        }*/

        private void OnDrop(object sender, DragEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isEditing) return;

           // TreeListView row = UIHelpers.TryFindFromPoint<TreeListView>((UIElement)sender, e.GetPosition(AssociatedObject));
           //// if (row == null || row.IsEditing) return;

            //set flag that indicates we're capturing mouse movements
            isDragging = true;
           //// draggedItem = row.SelectedRows;
        }

        private void OnDragLeave(object sender, DragEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public static void RemoveDragEndedHandler(DependencyObject d, RoutedEventHandler handler)
        {
            UIElement uie = d as UIElement;
            if (uie != null)
                uie.RemoveHandler(AllAddressDragDropManager.DragEndedEvent, handler);
        }

        private void RaiseDragEndedEvent()
        {
            var args = new RoutedEventArgs(AllAddressDragDropManager.DragEndedEvent);
           // AssociatedObject.RaiseEvent(args);
        }
        #endregion
        public void OnStartDrag(object sender, TreeListStartDragEventArgs e)
        {
            //isEditing = true;
            //in case we are in the middle of a drag/drop operation, cancel it...
            if (isDragging) ResetDragDrop();
        }

        private void ResetDragDrop()
        {
            isDragging = false;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            PopupBase Popup = new PopupBase();
            Popup.SetSize(new Size(50, 50));
            Popup.Placement = System.Windows.Controls.Primitives.PlacementMode.RelativePoint;
            //if (!isDragging || e.LeftButton != MouseButtonState.Pressed)
                //return;
            Popup.DataContext = draggedItem;
            //display the popup if it hasn't been opened yet
            if (!Popup.IsOpen)
            {
                //switch to read-only mode
                //AssociatedObject.AllowEditing = true;

                //make sure the popup is visible
                Popup.IsOpen = true;
            }

            var popupSize = new Size(Popup.ActualWidth, Popup.ActualHeight);
          //  Popup.PlacementRectangle = new Rect(e.GetPosition(AssociatedObject), popupSize);

            //make sure the row under the grid is being selected
           // var position = e.GetPosition(AssociatedObject);
            //var row = UIHelpers.TryFindFromPoint<TreeListView>(AssociatedObject, position);
            ///////if (row != null) AssociatedObject.SelectedRows = row.Item;
        }
    }
    public static class UIHelpers
    {

        #region find parent

        /// <summary>
        /// Finds a parent of a given item on the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="child">A direct or indirect child of the
        /// queried item.</param>
        /// <returns>The first parent item that matches the submitted
        /// type parameter. If not matching item can be found, a null
        /// reference is being returned.</returns>
        public static T TryFindParent<T>(DependencyObject child)
          where T : DependencyObject
        {
            //get parent item
            DependencyObject parentObject = GetParentObject(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                //use recursion to proceed with next level
                return TryFindParent<T>(parentObject);
            }
        }


        /// <summary>
        /// This method is an alternative to WPF's
        /// <see cref="VisualTreeHelper.GetParent"/> method, which also
        /// supports content elements. Do note, that for content element,
        /// this method falls back to the logical tree of the element.
        /// </summary>
        /// <param name="child">The item to be processed.</param>
        /// <returns>The submitted item's parent, if available. Otherwise
        /// null.</returns>
        public static DependencyObject GetParentObject(DependencyObject child)
        {
            if (child == null) return null;
            ContentElement contentElement = child as ContentElement;

            if (contentElement != null)
            {
                DependencyObject parent = ContentOperations.GetParent(contentElement);
                if (parent != null) return parent;

                FrameworkContentElement fce = contentElement as FrameworkContentElement;
                return fce != null ? fce.Parent : null;
            }

            //if it's not a ContentElement, rely on VisualTreeHelper
            return VisualTreeHelper.GetParent(child);
        }

        #endregion


        #region update binding sources

        /// <summary>
        /// Recursively processes a given dependency object and all its
        /// children, and updates sources of all objects that use a
        /// binding expression on a given property.
        /// </summary>
        /// <param name="obj">The dependency object that marks a starting
        /// point. This could be a dialog window or a panel control that
        /// hosts bound controls.</param>
        /// <param name="properties">The properties to be updated if
        /// <paramref name="obj"/> or one of its childs provide it along
        /// with a binding expression.</param>
        public static void UpdateBindingSources(DependencyObject obj,
                                  params DependencyProperty[] properties)
        {
            foreach (DependencyProperty depProperty in properties)
            {
                //check whether the submitted object provides a bound property
                //that matches the property parameters
                System.Windows.Data.BindingExpression be = BindingOperations.GetBindingExpression(obj, depProperty);
                if (be != null) be.UpdateSource();
            }

            int count = VisualTreeHelper.GetChildrenCount(obj);
            for (int i = 0; i < count; i++)
            {
                //process child items recursively
                DependencyObject childObject = VisualTreeHelper.GetChild(obj, i);
                UpdateBindingSources(childObject, properties);
            }
        }

        #endregion


        /// <summary>
        /// Tries to locate a given item within the visual tree,
        /// starting with the dependency object at a given position. 
        /// </summary>
        /// <typeparam name="T">The type of the element to be found
        /// on the visual tree of the element at the given location.</typeparam>
        /// <param name="reference">The main element which is used to perform
        /// hit testing.</param>
        /// <param name="point">The position to be evaluated on the origin.</param>
        public static T TryFindFromPoint<T>(UIElement reference, Point point)
          where T : DependencyObject
        {
            DependencyObject element = reference.InputHitTest(point)
                                         as DependencyObject;
            if (element == null) return null;
            else if (element is T) return (T)element;
            else return TryFindParent<T>(element);
        }
    }
}

