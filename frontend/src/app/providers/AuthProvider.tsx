import { createContext, useContext, useMemo, useState } from 'react';

type AuthContextType = {
  token: string | null;
  permissions: string[];
  setToken: (value: string | null) => void;
};

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: { children: React.ReactNode }) => {
  const [token, setToken] = useState<string | null>(null);
  const permissions = useMemo(() => ['product.read', 'product.write'], []);

  return (
    <AuthContext.Provider value={{ token, permissions, setToken }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) throw new Error('useAuth must be used within AuthProvider');
  return context;
};
