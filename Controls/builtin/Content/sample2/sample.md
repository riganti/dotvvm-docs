## Sample 2: Viewmodel Inheritance

Typically, the viewmodel of the content page derives the viewmodel of the master page. Note that the viewmodel of the master page can be an interface.

Notice that the `PageViewModel` class derives from the `BaseViewModel`. Also, all properties referenced in bindings in the master page must be declared
in the `BaseViewModel` class.