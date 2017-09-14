

export interface RouteInfo {
    id: string;
    path: string;
    title: string;
    miniTitle: string;
    icon: string;
    children?: RouteInfo[];
}
