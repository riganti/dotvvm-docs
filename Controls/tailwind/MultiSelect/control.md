A searchable selector which lets the user pick multiple values. The selected values are displayed as chips inside the input, and the suggestions can be filtered by typing.

The control uses the same binding conventions as the Tailwind [ComboBox](~/controls/tailwind/ComboBox). The selected values keep their original types (numbers, enums, strings, or objects). Use [MultiSelectFormField](~/controls/tailwind/Form) when you need a label and a validation message.

The control supports keyboard navigation: Alt+Down opens the suggestions, arrow keys move between them, and Enter selects the active item. Backspace at the start of the input removes the last selected value.

> The `MultiSelect` control is new in DotVVM 5.1.
