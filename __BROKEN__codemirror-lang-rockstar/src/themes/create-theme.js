import { EditorView } from '@codemirror/view';
import { HighlightStyle, syntaxHighlighting, } from '@codemirror/language';
const createTheme = ({ variant, settings, styles }) => {
    const theme = EditorView.theme({
        // eslint-disable-next-line @typescript-eslint/naming-convention
        '&': {
            backgroundColor: settings.background,
            color: settings.foreground,
        },
        '.cm-content': {
            caretColor: settings.caret,
        },
        '.cm-cursor, .cm-dropCursor': {
            borderLeftColor: settings.caret,
        },
        '&.cm-focused .cm-selectionBackgroundm .cm-selectionBackground, .cm-content ::selection': {
            backgroundColor: settings.selection,
        },
        '.cm-activeLine': {
            backgroundColor: settings.lineHighlight,
        },
        '.cm-gutters': {
            backgroundColor: settings.gutterBackground,
            color: settings.gutterForeground,
        },
        '.cm-activeLineGutter': {
            backgroundColor: settings.lineHighlight,
        },
    }, {
        dark: variant === 'dark',
    });
    const highlightStyle = HighlightStyle.define(styles);
    const extension = [theme, syntaxHighlighting(highlightStyle)];
    return extension;
};
export default createTheme;
