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
* DotVVM Visual Studio Extension

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

Sections should be level 1 headings `# New features` while individual entries should be level 3 headings `### Cool new feature`.  
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

The heading levels are shifted:
- Version — level 1
- Section (e.g. Changes to existing controls) — level 2
- Change entries (e.g. RadioButton) — level 3 
- individual changes — list items

Breaking changes should be marked by prefixing change entry by `**BREAKING CHANGE**`.

Resulting structure should look something like this.
```
# 2.4.0.10
## New controls
- ### [```Icon```](https://www.dotvvm.com/docs/controls/bootstrap4/Icon)
    
## Changes to existing controls
### **Toast**
- Fixed bug in *OnShown* and *OnHide* events.
-  *OnShown* and *OnHide* events are no longer called immediately after page load. Those events are now called only after client side change.

### Other changes
- General bug fixes in Custom CSS feature.
```

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
