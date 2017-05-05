using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using Telerik.Windows.Controls;
using System.Collections.Specialized;
using System.Windows;
using System.Collections;

namespace WpfApplication1
{
    public class MyMultiSelectBehavior : Behavior<RadGridView>
    {
        private RadGridView Grid
        {
            get
            {
                return AssociatedObject as RadGridView;
            }
        }

        public INotifyCollectionChanged SelectedItems
        {
            get { return (INotifyCollectionChanged)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItemsProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems", typeof(INotifyCollectionChanged), typeof(MyMultiSelectBehavior), new PropertyMetadata(OnSelectedItemsPropertyChanged));


        private static void OnSelectedItemsPropertyChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            var behavior = (MyMultiSelectBehavior)target;

            var oldCollection = args.OldValue as INotifyCollectionChanged;
            if (oldCollection != null)
            {
                oldCollection.CollectionChanged -= behavior.ContextSelectedItems_CollectionChanged;
            }

            var collection = args.NewValue as INotifyCollectionChanged;
            if (collection != null)
            {
                if (behavior.Grid != null && behavior.Grid.SelectedItems != null)
                {
                    behavior.Grid.SelectedItems.CollectionChanged -= behavior.GridSelectedItems_CollectionChanged;
                    Transfer(collection as IList, behavior.Grid.SelectedItems);
                    behavior.Grid.SelectedItems.CollectionChanged += behavior.GridSelectedItems_CollectionChanged;
                }
                collection.CollectionChanged += behavior.ContextSelectedItems_CollectionChanged;
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            Grid.SelectedItems.CollectionChanged += GridSelectedItems_CollectionChanged;
        }

        void ContextSelectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                UnsubscribeFromEvents();

                Transfer(SelectedItems as IList, Grid.SelectedItems);

                SubscribeToEvents();
            }));
        }

        void GridSelectedItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {

                UnsubscribeFromEvents();

                Transfer(Grid.SelectedItems, SelectedItems as IList);

                SubscribeToEvents();
            }));
        }

        private void SubscribeToEvents()
        {
            if (Grid != null && Grid.SelectedItems != null)
            {
                Grid.SelectedItems.CollectionChanged += GridSelectedItems_CollectionChanged;
            }

            if (SelectedItems != null)
            {
                SelectedItems.CollectionChanged += ContextSelectedItems_CollectionChanged;
            }
        }

        private void UnsubscribeFromEvents()
        {
            if (Grid != null && Grid.SelectedItems != null)
            {
                Grid.SelectedItems.CollectionChanged -= GridSelectedItems_CollectionChanged;
            }

            if (SelectedItems != null)
            {
                SelectedItems.CollectionChanged -= ContextSelectedItems_CollectionChanged;
            }
        }

        public static void Transfer(IList source, IList target)
        {
            if (source == null || target == null)
                return;

            target.Clear();

            foreach (var o in source)
            {
                target.Add(o);
            }
        }
    }
}
