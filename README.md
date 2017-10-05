# XamarinShopPractice
I wanna learn C# and some Xamarin libraries. Why not do both at the same time with a practice project?

Waffle Badge: [![Stories in Ready](https://badge.waffle.io/xavierliancw/XamarinShopPractice.png?label=ready&title=Ready)](http://waffle.io/xavierliancw/XamarinShopPractice)

## C# Things Learned:
_Through Commit:_ c6149061b94bfbabacdc04b68041f9bedbccf3a1
- Asynchronous tasks
- Asynchronous code
- Accessors
- Reference types vs value types (class vs struct)
- How to generate GUIDs (not just Guid id = new System.Guid() //no, no, no it's not like Swift)
- Some minor things about Generics (I can use them without fully understanding everything about them)
- Naming convention: Interface names start with an "I"
- Naming convention: Private variables start with an "_"

_Through Commit:_ b7ad704d5489baf8cbb0cd23de4a1daf5bea149f
- C# inheritance (override vs new, virtual, etc.)
- How to use try-catch blocks
- Tuples
- Tuples are immutable!

_Through Commit:_ 45e2d2a9d08a271223f0593fb9f9c33f0acb74cd
- Nullables!
- Namespace separation to facilitate separation of concerns
- Static classes and static methods

_Through Commit:_ af3042f72f97e8bb14418c1e10ac4b9ee1e91e38
- Bindable properties (kind of... still kind of shakey on the whole idea)

## Xamarin Things Learned:
_Through Commit:_ c6149061b94bfbabacdc04b68041f9bedbccf3a1
- Persisting cross-platform data through a SQLite database
- Installing NuGet packages (dead. simple.)
- Some small skills that help find the RIGHT NuGet package out of the myriad of choices
- A single NuGet package needs to be installed separately on every platform (Forms, iOS, and Android)
- How to update the Android Simulator OS through Visual Studio settings
- I'm spoiled because Googling help for Swift is x10 easier than Googling help for Xamarin

_Through Commit:_ b7ad704d5489baf8cbb0cd23de4a1daf5bea149f
- What Object-Relational Mapping means
- What #region #endregion is

_Through Commit:_ af3042f72f97e8bb14418c1e10ac4b9ee1e91e38
- What embedded resources are
- How to add and display an embedded image
- Xamarin XAML can't display embedded images using their names out of the box, so you have to add your own extension
- Custom renderers!
- Bindable properties through custom renderers
- Xamarin animations with XAML/codebehind classes particularly FadeTo(), TranslateTo()
- How to edit and work with XAML enough for most situations (proficient, not a master)
- Grid layouts!
- ForceUpdate() (ContentPage) calls OnSizeAllocated(double width, double height)
- OnSizeAllocated(...) is overridable and is kind of like iOS' layoutSubviews()
- When trying to set up UI sizes and stuff, use OnSizeAllocated(...)
