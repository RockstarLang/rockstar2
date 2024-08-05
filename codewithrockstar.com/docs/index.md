---
title: Docs Home
layout: home
examples: /examples/01-getting-started/
nav_exclude: false
nav_order: "0000"

---

Welcome!

This is the official documentation for Rockstar v__VERSION__.

<ul id="index-nav">
{% assign contents = site.pages | where_exp:"item", "item.summary != nil" %}
{% for page in contents %}
    <li>
        <a href="{{ page.url | relative_url }}">{{ page.title }}</a>
        <p>{{ page.summary }}</p>
</li>
{% endfor %}
</ul>
