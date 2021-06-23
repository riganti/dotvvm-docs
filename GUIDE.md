# DotVVM documentation guide

Hello and welcome to the DotVVM documentation guide. This document describes the conventions we strive to follow when
writing documentation for DotVVM. Since they are still a work-in-progress, some section may be incomplete.

As with any set of conventions, consistency is more important than individual preference. In DotVVM docs, we stick to
the following:

## US English

**Use American English.**

A lot of the differences between the two language variants follow certain patterns (which don't always apply):

| American English | British English | Pattern      |
|------------------|-----------------|--------------|
| Color            | Colour          | -our -> -or  |
| Analyze          | Analyse         | -yse -> -yze |
| Center           | Centre          | -re -> -er   |
| Fulfill          | Fulfil          | -il -> -ill  |
| Dialog           | Dialogue        | -ogue -> -og |

Despite these patterns, it's always a good idea to use a spellchecker or a dictionary. See the _Tools_ section below
for tips on which to use.

## Oxford comma

**Use the Oxford comma.**

The Oxford comma is the comma before the “and” in front of the last item in an enumeration:

> To create this form, we used the TextBox, ComboBox, and Button controls.

In some cases it can resolve ambiguity and is suitable for formal settings such as a documentation.

## Capitalization

**Use sentence capitalization for the titles of pages.**

English capitalization follows a much more lenient set of rules than Czech or Slovak capitalization.
The most important ones are:

* The first word of a sentence begins with a capital letter.
* _Proper nouns_ such as the names of people, cities, days, etc. have the first letter of every word capitalized
  **except** for prepositions (e.g. “for”, “of”, “in”) and articles (“a”, “the”).

While the titles of books are proper nouns, in these docs we treat the titles of pages as sentences.

### List of DotVVM proper nouns

* DotVVM
* DotHTML
* DotVVM Command Line
* DotVVM for Visual Studio
* Bootstrap for DotVVM

### List of other related proper nouns

* JavaScript
* IntelliSense
* TypeScript
* OWIN
* LINQ

## Typography

**Use the better-looking Unicode characters.**

| Kind                  | Character | Unicode code point | Alt Code | HTML     |
|-----------------------|-----------|--------------------|----------|----------|
| left quotation mark   | “         | U+201C             | 0147     | &ldquo\; |
| right quotation mark  | ”         | U+201D             | 0148     | &rdquo\; |
| m-dash                | —         | U+2015             | 0151     | &#8213\; |


## Release notes format
Release notes should be split into several sections:
- New features
- Improvements and fixes
- Breaking changes
- ...

Sections should be level 2 headings `## New features` while individual entries should be level 3 headings `### Cool new feature`.  
This division provides the best distinction between individual sections.

Format of each entry should be as follows:
```
### Entry name not ending by .
#123 <Pull request ID.>  
Brief description of the new feature.  
[Link to doc](https://www.dotvvm.com/docs/tutorials/basics-routing/)
```

[Example of release notes](https://github.com/riganti/dotvvm/releases/tag/v3.0)

### Control packs release notes 
Control packs do not have release notes split into multiple documents, but all versions share the same file.  
Due to this difference the guidelines slightly differ.
- Page heading (e.g. Release notes) — level 1
- Version — level 2
- Section (e.g. Changes to existing controls) — level 3
- Change entries (e.g. RadioButton) — level 4
- individual changes — list items

Breaking changes should be marked by prefixing change entry by `**BREAKING CHANGE**`.

Resulting structure should look something like this.
```
# Release notes

## 2.4.0.10
### New controls
#### [Icon](https://www.dotvvm.com/docs/controls/bootstrap4/Icon)
    
### Changes to existing controls
#### **Toast**
- Fixed bug in *OnShown* and *OnHide* events.
-  *OnShown* and *OnHide* events are no longer called immediately after page load. Those events are now called only after client side change.

#### Other changes
- General bug fixes in Custom CSS feature.
```


## Headings and page structure

* Each page must have exactly one first-level heading (`h1`). It must be equal to the title of the page in `menu.xml`.
* Each page should have a correct hierarchy of headings (no skipping of levels). The table of contents on the right side is generated from headings of the second and third level. Please make sure that the hierarchy of headings in the page makes sense and is easy to navigate - some users will not read the entire page but look in the TOC to find the section they need. 
* Prefer imperative form of verbs over the "-ing" form in headings to express what the user will achieve in the section: _"Register a markup control"_ looks better than _"Registering a markup control"_.
* Each page must end with a last second-level section called _"See also"_ with an unordered list of links to other relevant pages or controls.

## Links within the docs

We assume that most links between the pages will want to link to the same version of the framework. Therefore, we can use `~` which stands for the documentation repository root (and is replaced with `/docs/{current-version}/` when the page is rendered).

* To link to a docs, use the following format:

```
[Master pages](~/pages/concepts/layout/master-pages)
``` 

* To link to a control, use the following format:

```
[Button](~/controls/builtin/Button)
```

* If you need to link to a specific version of the documentation, use the slash-rooted URL in the following format:

```
[RouteLink](/docs/2.0/controls/builtin/RouteLink)
```

## Images

* To use image in the page, place the image file in the same folder as the page (ideally with the same name and suffix `_img1`, and so on). Make sure the image title express what's shown on the image.

```
![Click on Create a new project on the launch screen](create-new-project_img1.png)
```

## Terms

If you use a term (like viewmodel, postback etc.) on a page **for the first time**, it should:

* link to a page where the term is explained, if such a page exists (both externally, or internally),
* it should be typed **in bold**.

The subsequent occurrences of the term should not be a link and should not be typed in bold.

It is a good idea to repeat the link in the "See also" section at the end of the page.

## Names which appear in code

If you use a name that appears in the code (class, property, method, and so on), it should be always typed in `monospaced font`. We do not use the monospaced font only if the name is a link to another page. 

The subsequent occurrences of such name should not be links, but they should be typed in `monospaced font`.

It is a good idea to repeat the link in the "See also" section at the end of the page.


## Tools

**[LTeX](https://marketplace.visualstudio.com/items?itemName=valentjn.vscode-ltex)**
* A buffed-up VS Code spellchecker that understands Markdown syntax.
* Can even spot errors in common phrases with its [n-gram support](https://dev.languagetool.org/finding-errors-using-n-gram-data.html).
* Take advantage of the DotVVM-specific `.vscode\ltex.dictionary.en-US.txt` dictionary.

**[Grammarly](https://www.grammarly.com/)**
* A smart spellchecker that even checks the tone of your text.
* However, it doesn't understand Markdown, so working with the docs can be tedious.
* At the very least Grammarly also provides nice [overviews of English grammar](https://www.grammarly.com/blog/).

**[Lexico](https://www.lexico.com)**
* A dictionary that explicitly states whether a word belongs to British English or American English.

**[Google Ngram Viewer](https://books.google.com/ngrams)**
* Word data collected from books.
* Use it when you're unsure which variant of a word is used more often (e.g. “self-reference” vs “selfreference”).

**[Seznam Slovník](https://slovnik.seznam.cz)**
* A Czech <-> English dictionary that lists verbs in all tenses.
* Also, useful for finding synonyms.

## Reference

**Docs style and voice quick start**
* https://docs.microsoft.com/en-us/contribute/style-quick-start

**Capitalization**
* https://www.grammarly.com/blog/capitalization-rules/

**What Is the Oxford Comma (or Serial Comma)?**
* https://www.grammarly.com/blog/what-is-the-oxford-comma-and-why-do-people-care-so-much-about-it/
