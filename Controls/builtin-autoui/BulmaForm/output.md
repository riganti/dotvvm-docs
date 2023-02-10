```DOTHTML
<div class="field is-horizontal">
  <div class="field-label is-normal autoui-required">
    <label for="Name__input" class="label">Person or company name</label>
  </div>
  <div class="field-body">
    <div class="field">
      <div class="control">
        <input type="text" required class="input" id="Name__input" ... />
      </div>
      <span class="help is-danger" ...>Validation message</span>
    </div>
  </div>
</div>
<div class="field is-horizontal">
  <div class="field-label is-normal"></div>
  <div class="field-body">
    <div class="field">
      <div class="control">
        <label class="checkbox" id="IsCompany__input" ...>
          <input type="checkbox" data-bind="dotvvm-CheckState: IsCompany" />
          <span>Is company</span>
        </label>
      </div>
      <span class="help is-danger" ...>Validation message</span>
    </div>
  </div>
</div>
```