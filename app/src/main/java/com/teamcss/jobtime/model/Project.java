package com.teamcss.jobtime.model;

public class Project {
    private String id;
    private String clientId;
    private String name;
    private int salesHours;

    public Project(String id, String clientId, String name, int salesHours) {
        this.id = id;
        this.clientId = clientId;
        this.name = name;
        this.salesHours = salesHours;
    }

    public String getId() { return id; }

    public void setId(String id) {
        this.id = id;
    }

    public String getClientId() {
        return clientId;
    }

    public void setClientId(String clientId) {
        this.clientId = clientId;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getSalesHours() {
        return salesHours;
    }

    public void setSalesHours(int salesHours) {
        this.salesHours = salesHours;
    }

    @Override
    public String toString() {
        return this.name;
    }
}