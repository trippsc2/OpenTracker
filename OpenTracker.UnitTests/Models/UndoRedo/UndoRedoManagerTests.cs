using Autofac;
using NSubstitute;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo
{
    public class UndoRedoManagerTests
    {
        private readonly ISaveLoadManager _saveLoadManager = Substitute.For<ISaveLoadManager>();
        private readonly UndoRedoManager _sut;

        public UndoRedoManagerTests()
        {
            _sut = new UndoRedoManager(_saveLoadManager);
        }

        [Fact]
        public void NewAction_ShouldCallCanExecute()
        {
            var action = Substitute.For<IUndoable>();
            _sut.NewAction(action);

            action.Received().CanExecute();
        }

        [Fact]
        public void NewAction_ShouldDoNothing_WhenCanExecuteReturnsFalse()
        {
            var action = Substitute.For<IUndoable>();
            action.CanExecute().Returns(false);
            _sut.NewAction(action);
            
            action.DidNotReceive().ExecuteDo();
        }

        [Fact]
        public void NewAction_ShouldCallExecuteDo_WhenCanExecuteReturnsTrue()
        {
            var action = Substitute.For<IUndoable>();
            action.CanExecute().Returns(true);
            _sut.NewAction(action);
            
            action.Received().ExecuteDo();
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void NewAction_ShouldSetCanUndoToTrue(bool expected, bool canExecute)
        {
            var action = Substitute.For<IUndoable>();
            action.CanExecute().Returns(canExecute);
            _sut.NewAction(action);
            
            Assert.Equal(expected, _sut.CanUndo);
        }

        [Fact]
        public void NewAction_ShouldRaisePropertyChanged_WhenCanUndoChanged()
        {
            var action = Substitute.For<IUndoable>();
            action.CanExecute().Returns(true);
            
            Assert.PropertyChanged(_sut, nameof(IUndoRedoManager.CanUndo), () => _sut.NewAction(action));
        }

        [Fact]
        public void NewAction_ShouldSetCanRedoToFalse()
        {
            var action1 = Substitute.For<IUndoable>();
            var action2 = Substitute.For<IUndoable>();
            var action3 = Substitute.For<IUndoable>();
            
            action1.CanExecute().Returns(true);
            action2.CanExecute().Returns(true);
            action3.CanExecute().Returns(true);

            _sut.NewAction(action1);
            _sut.NewAction(action2);
            _sut.Undo();
            _sut.NewAction(action3);
            
            Assert.False(_sut.CanRedo);
        }
        
        [Fact]
        public void NewAction_ShouldRaisePropertyChanged_WhenCanRedoChanged()
        {
            var action1 = Substitute.For<IUndoable>();
            var action2 = Substitute.For<IUndoable>();
            var action3 = Substitute.For<IUndoable>();
            
            action1.CanExecute().Returns(true);
            action2.CanExecute().Returns(true);
            action3.CanExecute().Returns(true);

            _sut.NewAction(action1);
            _sut.NewAction(action2);
            _sut.Undo();
            
            Assert.PropertyChanged(_sut, nameof(IUndoRedoManager.CanRedo), () => _sut.NewAction(action3));
        }

        [Fact]
        public void NewAction_ShouldSetUnsavedToTrue()
        {
            var action = Substitute.For<IUndoable>();
            action.CanExecute().Returns(true);
            _sut.NewAction(action);
            
            Assert.True(_saveLoadManager.Unsaved);
        }

        [Fact]
        public void Undo_ShouldNotChangeUnsaved_WhenUndoableActionsIsEmpty()
        {
            _sut.Undo();
            
            Assert.False(_saveLoadManager.Unsaved);
        }

        [Fact]
        public void Undo_ShouldSetCanUndoToFalse_WhenUndoableActionsHasOnlyOneMember()
        {
            var action = Substitute.For<IUndoable>();
            action.CanExecute().Returns(true);
            _sut.NewAction(action);
            _sut.Undo();
            
            Assert.False(_sut.CanUndo);
        }

        [Fact]
        public void Undo_ShouldCallExecuteUndo()
        {
            var action = Substitute.For<IUndoable>();
            action.CanExecute().Returns(true);
            _sut.NewAction(action);
            _sut.Undo();
            
            action.Received().ExecuteUndo();
        }

        [Fact]
        public void Undo_ShouldSetCanRedoToTrue()
        {
            var action = Substitute.For<IUndoable>();
            action.CanExecute().Returns(true);
            _sut.NewAction(action);
            _sut.Undo();
            
            Assert.True(_sut.CanRedo);
        }

        [Fact]
        public void Undo_ShouldSetUnsavedToTrue()
        {
            var action = Substitute.For<IUndoable>();
            action.CanExecute().Returns(true);
            _sut.NewAction(action);
            _saveLoadManager.Unsaved.Returns(false);
            _sut.Undo();
            
            Assert.True(_saveLoadManager.Unsaved);
        }

        [Fact]
        public void Redo_ShouldNotChangeUnsaved_WhenRedoableActionsIsEmpty()
        {
            _sut.Redo();
            
            Assert.False(_saveLoadManager.Unsaved);
        }

        [Fact]
        public void Redo_ShouldSetCanRedoToFalse_WhenRedoableActionsHasOnlyOneMember()
        {
            var action = Substitute.For<IUndoable>();
            action.CanExecute().Returns(true);
            _sut.NewAction(action);
            _sut.Undo();
            _sut.Redo();
            
            Assert.False(_sut.CanRedo);
        }

        [Fact]
        public void Redo_ShouldCallExecuteDo()
        {
            var action = Substitute.For<IUndoable>();
            action.CanExecute().Returns(true);
            _sut.NewAction(action);
            _sut.Undo();
            _sut.Redo();
            
            action.Received(2).ExecuteDo();
        }

        [Fact]
        public void Redo_ShouldSetCanUndoToTrue()
        {
            var action = Substitute.For<IUndoable>();
            action.CanExecute().Returns(true);
            _sut.NewAction(action);
            _sut.Undo();
            _sut.Redo();
            
            Assert.True(_sut.CanUndo);
        }

        [Fact]
        public void Redo_ShouldSetUnsavedToTrue()
        {
            var action = Substitute.For<IUndoable>();
            action.CanExecute().Returns(true);
            _sut.NewAction(action);
            _sut.Undo();
            _saveLoadManager.Unsaved.Returns(false);
            _sut.Redo();
            
            Assert.True(_saveLoadManager.Unsaved);
        }

        [Fact]
        public void Reset_ShouldSetCanUndoToFalse()
        {
            var action = Substitute.For<IUndoable>();
            action.CanExecute().Returns(true);
            _sut.NewAction(action);
            _sut.Reset();
            
            Assert.False(_sut.CanUndo);
        }

        [Fact]
        public void Reset_ShouldSetCanRedoToFalse()
        {
            var action = Substitute.For<IUndoable>();
            action.CanExecute().Returns(true);
            _sut.NewAction(action);
            _sut.Redo();
            _sut.Reset();
            
            Assert.False(_sut.CanRedo);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<ISaveLoadManager>();
            
            Assert.NotNull(sut as SaveLoadManager);
        }
    }
}