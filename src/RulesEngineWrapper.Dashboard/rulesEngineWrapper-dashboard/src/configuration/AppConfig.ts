export class AppConfig {
  private customUrl: string = "rulesEngine-dashboard";
  private initialized: boolean = false;

  async initialize(): Promise<AppConfig> {
    if (!this.initialized) {
      try {
        const response = await fetch("/getRulesEngineDashboardConfiguration");
        const config = await response.json();

        this.customUrl = config.CustomUrl;

        this.initialized = true;
      } catch (error) {
        console.error("Failed to initialize AppConfig:", error);
        throw error;
      }
    }
    return this;
  }

  getBaseUrl(): string {
    this.ensureInitialized();
    return this.customUrl;
  }

  private ensureInitialized() {
    if (!this.initialized) {
      throw new Error(
        "AppConfig not initialized. Call AppConfig.initialize() first."
      );
    }
  }
}
