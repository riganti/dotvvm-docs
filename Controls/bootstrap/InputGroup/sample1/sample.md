## Sample 1: Basic Usage

* The `InputGroup` control has the `ContentTemplate` property which must contain a [TextBox](~/controls/builtin/TextBox) control.

It also has the `LeftTemplate` and `RightTemplate` properties that define the content on the left and right of the textbox.

The following types of content are supported in these templates:

* Text content or single HTML inline element (e.g. `<span>`)
* [Button](~/controls/bootstrap/Button)
* [DropDownButton](~/controls/bootstrap/DropDownButton)
* [CheckBox](~/controls/bootstrap/CheckBox) 
* [RadioButton](~/controls/bootstrap/RadioButton) 
* [GlyphIcon](~/controls/bootstrap/GlyphIcon) 

There is also the `Size` property which can be set to `Large`, `Small` and `Default`.
