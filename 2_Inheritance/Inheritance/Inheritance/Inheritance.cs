namespace CompanyApp;

/// <summary>
/// Определяет контракт для сотрудников, обладающих управленческими функциями.
/// </summary>
public interface IManageable
{
    /// <summary>
    /// Осуществляет управление командой или подчиненными.
    /// </summary>
    void Manage();
}