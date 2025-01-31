import { deleteCookie } from 'cookies-next';
import { jwtDecode } from 'jwt-decode';
import { cookies } from 'next/headers';
import { MiddlewareConfig, NextRequest, NextResponse } from 'next/server';

const publicRoutes = [
  { path: '/sign-in', whenAuthenticated: 'redirect' },
  { path: '/register', whenAuthenticated: 'redirect' },
  { path: '/session-expired', whenAuthenticated: 'redirect' },
] as const;

const REDIRECT_WHEN_NOT_AUTHENTICATED = '/sign-in';
const PATH_DASHBOARD = '/';

export async function middleware(request: NextRequest) {
  const path = request.nextUrl.pathname;
  const publicRoute = publicRoutes.find((route) => route.path === path);
  const authToken = request.cookies.get('access_token');

  if (!authToken && publicRoute) {
    return NextResponse.next();
  }
  if (!authToken?.value) {
    const redirectUrl = request.nextUrl.clone();
    redirectUrl.pathname = REDIRECT_WHEN_NOT_AUTHENTICATED;

    return NextResponse.redirect(redirectUrl);
  }

  if (!authToken && !publicRoute) {
    const redirectUrl = request.nextUrl.clone();
    redirectUrl.pathname = REDIRECT_WHEN_NOT_AUTHENTICATED;
    return NextResponse.redirect(redirectUrl);
  }

  if (authToken && publicRoute && publicRoute.whenAuthenticated === 'redirect') {
    const redirectUrl = request.nextUrl.clone();
    redirectUrl.pathname = PATH_DASHBOARD;
    return NextResponse.redirect(redirectUrl);
  }

  const decoded = jwtDecode<{ exp: number }>(authToken?.value);

  const currentTime = Math.floor(Date.now() / 1000);
  const jwtExpired = decoded.exp < currentTime;

  if (jwtExpired === true && publicRoute?.whenAuthenticated != 'redirect') {
    (await cookies()).delete('access_token');

    const redirectUrl = request.nextUrl.clone();
    redirectUrl.pathname = '/session-expired';

    return NextResponse.redirect(redirectUrl);
  }

  return NextResponse.next();
}

export const config: MiddlewareConfig = {
  matcher: [
    /*
     * Match all request paths except for the ones starting with:
     * - api (API routes)
     * - _next/static (static files)
     * - _next/image (image optimization files)
     * - favicon.ico, sitemap.xml, robots.txt (metadata files)
     */
    '/((?!api|_next/static|_next/image|favicon.ico|sitemap.xml|robots.txt).*)',
  ],
};
