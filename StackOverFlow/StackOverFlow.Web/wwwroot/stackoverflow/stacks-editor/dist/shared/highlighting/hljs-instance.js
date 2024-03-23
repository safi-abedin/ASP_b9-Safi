import hljs from "highlight.js";
/* eslint-disable @typescript-eslint/ban-ts-comment */
/*
 * NOTE: This file is only for local development / generic bundles
 * The prod/stackoverflow build configs completely remove highlight.js from the bundle,
 * expecting it to be supplied globally as `window.hljs`, already configured and ready to go
 */
export function getHljsInstance() {
    var _a;
    // @ts-expect-error
    const hljsInstance = (_a = globalThis.hljs) !== null && _a !== void 0 ? _a : hljs;
    return hljsInstance && hljsInstance.highlight ? hljsInstance : null;
}
