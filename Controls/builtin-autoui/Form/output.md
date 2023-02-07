```DOTHTML
<table class="autoui-form-table">
  <tr>
    <td class="autoui-label autoui-required">
      <label for="Name__input">Person or company name</label>
    </td>
    <td class="autoui-editor">
      <input type="text" required id="Name__input" ... />
    </td>
  </tr>
  <tr>
    <!-- CheckBox editors doesn't use the label cell, the text is placed behind the control -->
    <td class="autoui-label"></td>
    <td class="autoui-editor">
      <label id="IsCompany__input">
        <input type="checkbox" ... >
        <span>Is company</span>
      </label>
    </td>
  </tr>
</table>
```