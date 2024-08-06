class RockstarInclude < Liquid::Tag
	# def initialize(_tag_name, markup, _parse_context)
	#   super
	#   @markup = markup.strip
	# end

	def render(context)
		page = context.registers[:page]
		expanded_path = Liquid::Template.parse(@markup).render(context).strip
		page_filename = File.basename(page['path'], '.*')
		root_path = File.expand_path(context.registers[:site].config['source'])
		file_path = File.join(root_path, 'examples', page_filename, expanded_path)
		puts file_path
		p file_path
 # bare_filename = File.basename(expanded_path)
#<a href="#{expanded_path}">#{bare_filename}</a>:
	  <<-ROCKSTAR
```rockstar
#{read_file(file_path, context)}
```
ROCKSTAR
	end

	def read_file(path, context)
	  file_read_opts = context.registers[:site].file_read_opts
	  File.read(path, **file_read_opts)
	end
  end

  Liquid::Template.register_tag('rockstar_include', RockstarInclude)