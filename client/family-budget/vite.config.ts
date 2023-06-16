import path from "path";

import react from "@vitejs/plugin-react";
import {defineConfig} from "vite";

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [react()],
    server: {
        port: 3000,
    },
    resolve: {
        alias: {
            "@": path.resolve("src"),
            "@assets": path.resolve("src/_assets"),
            "@styles": path.resolve("src/_styles"),
            "@base": path.resolve("src/_styles/base.scss"),
            "@colors": path.resolve("src/_styles/colors.scss"),
            "@app": path.resolve("src/app"),
            "@core": path.resolve("src/core"),
            "@shared": path.resolve("src/shared"),
            "@store": path.resolve("src/store"),
        }
    }
});
