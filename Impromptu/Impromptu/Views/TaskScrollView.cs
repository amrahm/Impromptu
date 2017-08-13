using Xamarin.Forms;

namespace Impromptu.Views {
    public class TaskScrollView : ScrollView {
        public static readonly BindableProperty IsScrollEnabledProperty =
            BindableProperty.Create(nameof(IsScrollEnabled), typeof(bool), typeof(TaskScrollView), true, BindingMode.Default);
        /// <summary>
        /// Gets or sets the Native Bouncy effect status
        /// </summary>
        public bool IsScrollEnabled {
            get => (bool)GetValue(IsScrollEnabledProperty);
            set => SetValue(IsScrollEnabledProperty, value);
        }

        public static readonly BindableProperty IsHorizontalScrollbarEnabledProperty =
            BindableProperty.Create(nameof(IsHorizontalScrollbarEnabled), typeof(bool), typeof(TaskScrollView), false, BindingMode.Default);
        /// <summary>
        /// Gets or sets the Horizontal scrollbar visibility
        /// </summary>
        public bool IsHorizontalScrollbarEnabled {
            get => (bool)GetValue(IsHorizontalScrollbarEnabledProperty);
            set => SetValue(IsHorizontalScrollbarEnabledProperty, value);
        }


        public static readonly BindableProperty IsVerticalScrollbarEnabledProperty =
            BindableProperty.Create(nameof(IsVerticalScrollbarEnabled), typeof(bool), typeof(TaskScrollView), false, BindingMode.Default);
        /// <summary>
        /// Gets or sets the Vertical scrollbar visibility
        /// </summary>
        public bool IsVerticalScrollbarEnabled {
            get => (bool)GetValue(IsVerticalScrollbarEnabledProperty);
            set => SetValue(IsVerticalScrollbarEnabledProperty, value);
        }


        public static readonly BindableProperty IsNativeBouncyEffectEnabledProperty =
            BindableProperty.Create(nameof(IsNativeBouncyEffectEnabled), typeof(bool), typeof(TaskScrollView), true, BindingMode.Default);
        /// <summary>
        /// Gets or sets the Native Bouncy effect status
        /// </summary>
        public bool IsNativeBouncyEffectEnabled {
            get => (bool)GetValue(IsNativeBouncyEffectEnabledProperty);
            set => SetValue(IsNativeBouncyEffectEnabledProperty, value);
        }
    }
}