/**
 * Describes the supported html tags
 * @see {@link https://meta.stackexchange.com/questions/1777/what-html-tags-are-allowed-on-stack-exchange-sites|Supported tags}
 */
export declare enum TagType {
    unknown = 0,
    comment = 1,
    strike = 2,//<del>, <s>, <strike>
    strong = 3,//<b>, <strong>
    emphasis = 4,//<i>, <em>
    hardbreak = 5,//<br>, <br/> (space agnostic)
    code = 6,
    link = 7,// <a> [href] [title]
    image = 8,// <img /> [src] [width] [height] [alt] [title]
    keyboard = 9,
    pre = 10,
    sup = 11,
    sub = 12,
    heading = 13,// <h1>, <h2>, <h3>, <h4>, <h5>, <h6> (support full set of valid h tags)
    paragraph = 14,
    horizontal_rule = 15,
    blockquote = 16,
    list_item = 17,
    ordered_list = 18,
    unordered_list = 19,
    description_details = 20,// <dd>
    description_list = 21,// <dl>
    description_term = 22
}
/**
 * Describes the supported attributes for each html tag
 * @see {@link https://meta.stackexchange.com/questions/1777/what-html-tags-are-allowed-on-stack-exchange-sites|Supported tags}
 */
export declare const supportedTagAttributes: {
    [key in TagType]?: string[];
};
/**
 * Collection of elements that are counted as "block" level elements
 * TODO change to a map for fast lookup?
 */
export declare const blockElements: TagType[];
/**
 * Collection of elements that are self-closing (e.g. <br/>)
 * TODO change to a map for fast lookup?
 */
export declare const selfClosingElements: TagType[];
