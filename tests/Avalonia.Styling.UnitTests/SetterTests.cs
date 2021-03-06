// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System.Reactive.Subjects;
using Moq;
using Avalonia.Controls;
using Avalonia.Data;
using Xunit;
using System;
using Avalonia.Controls.Templates;

namespace Avalonia.Styling.UnitTests
{
    public class SetterTests
    {
        [Fact]
        public void Cannot_Assign_Control_To_Value()
        {
            var target = new Setter();

            Assert.Throws<ArgumentException>(() => target.Value = new Border());
        }

        [Fact]
        public void Setter_Should_Apply_Binding_To_Property()
        {
            var control = new TextBlock();
            var subject = new BehaviorSubject<object>("foo");
            var descriptor = new InstancedBinding(subject);
            var binding = Mock.Of<IBinding>(x => x.Initiate(control, TextBlock.TextProperty, null) == descriptor);
            var style = Mock.Of<IStyle>();
            var setter = new Setter(TextBlock.TextProperty, binding);

            setter.Apply(style, control, null);

            Assert.Equal("foo", control.Text);
        }

        [Fact]
        public void Setter_Should_Materialize_Template_To_Property()
        {
            var control = new Decorator();
            var template = new FuncTemplate<Canvas>(() => new Canvas());
            var style = Mock.Of<IStyle>();
            var setter = new Setter(Decorator.ChildProperty, template);

            setter.Apply(style, control, null);

            Assert.IsType<Canvas>(control.Child);
        }

        [Fact]
        public void Materializes_Template_Should_Be_NameScope()
        {
            var control = new Decorator();
            var template = new FuncTemplate<Canvas>(() => new Canvas());
            var style = Mock.Of<IStyle>();
            var setter = new Setter(Decorator.ChildProperty, template);

            setter.Apply(style, control, null);

            Assert.NotNull(NameScope.GetNameScope((Control)control.Child));
        }
    }
}
