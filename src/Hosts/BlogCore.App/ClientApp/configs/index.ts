export interface GlobalConfig {
  authorityServer: string;
  apiServer: string;
}

const windowIfDefined = typeof window === 'undefined' ? null : window as any;
const url =
  windowIfDefined &&
  `${window.location.protocol}//${window.location.hostname}:8484`;

export const globalConfig: GlobalConfig = {
  authorityServer: url,
  apiServer: url
};
