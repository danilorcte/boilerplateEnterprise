import { Navigate, Route, Routes } from 'react-router-dom';
import { ProductListPage } from '../../modules/products/pages/ProductListPage';
import { useAuth } from '../providers/AuthProvider';

const ProtectedRoute = ({ permission, children }: { permission: string; children: JSX.Element }) => {
  const { token, permissions } = useAuth();

  if (!token) return <Navigate to="/login" replace />;
  if (!permissions.includes(permission)) return <Navigate to="/unauthorized" replace />;

  return children;
};

export const AppRoutes = () => (
  <Routes>
    <Route
      path="/products"
      element={
        <ProtectedRoute permission="product.read">
          <ProductListPage />
        </ProtectedRoute>
      }
    />
    <Route path="*" element={<Navigate to="/products" replace />} />
  </Routes>
);
